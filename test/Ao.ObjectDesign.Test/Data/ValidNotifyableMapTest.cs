﻿using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class ValidNotifyableMapTest
    {
        class ValueNotifyableSetValidater : INotifyableSetValidater<string, VarValue>
        {
            public bool IsStopValidate { get; set; }
            public bool IsSkipGlobalValidate { get; set; }
            public bool IsSkipWithKeyValidate { get; set; }


            public Func<DataChangedEventArgs<string, VarValue>, bool> Func { get; set; }

            public bool Validate(DataChangedEventArgs<string, VarValue> e, ref NotifyableSetValidaterContext context)
            {
                if (IsStopValidate)
                {
                    context.StopValidate();
                }
                if (IsSkipGlobalValidate)
                {
                    context.SkipGlobalValidate();
                }
                if (IsSkipWithKeyValidate)
                {
                    context.SkipWithKeyValidate();
                }

                if (Func is null)
                {
                    return true;
                }
                return Func(e);
            }
        }
        [TestMethod]
        public void NewWithArguments()
        {
            new ValidNotifyableMap<string, VarValue>();
            new ValidNotifyableMap<string, VarValue>(10, 100);
            new ValidNotifyableMap<string, VarValue>(StringComparer.OrdinalIgnoreCase);
            var map = new ValidNotifyableMap<string, VarValue>(new Dictionary<string, VarValue>
            {
                ["a"] = VarValue.Byte0Value
            });
            var v = map["a"];
            Assert.AreEqual(VarValue.Byte0Value, v);
        }
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            Assert.ThrowsException<ArgumentNullException>(() => map.IsRegistdWithKeyRule("1", null));
            Assert.ThrowsException<ArgumentNullException>(() => map.IsRegistGlobal(null));
            Assert.ThrowsException<ArgumentNullException>(() => map.RegistGlobalRule(null));
            Assert.ThrowsException<ArgumentNullException>(() => map.RegistWithKeyRule("1", null));
            Assert.ThrowsException<ArgumentNullException>(() => map.UnRegistGlobalRule(null));
            Assert.ThrowsException<ArgumentNullException>(() => map.UnRegistWithKeyRule("1", null));
        }
        [TestMethod]
        public void RegistValidate()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            Assert.IsFalse(map.IsGlobalRulesCreated);
            Assert.IsFalse(map.IsWithKeyRulesCreated);
            map.RegistGlobalRule(valid);
            Assert.IsTrue(map.IsGlobalRulesCreated);
            Assert.IsNotNull(map.GlobalRules);
            map.RegistWithKeyRule("hello", valid);
            Assert.IsTrue(map.IsWithKeyRulesCreated);
            Assert.IsNotNull(map.WithKeyRules);
        }
        [TestMethod]
        public void Regist_UnRegist_Contains_Validate()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            Assert.IsFalse(map.IsRegistdWithKeyRule("a", valid));
            Assert.IsFalse(map.IsRegistGlobal(valid));

            map.RegistGlobalRule(valid);
            Assert.IsTrue(map.IsRegistGlobal(valid));
            var ok = map.UnRegistGlobalRule(valid);
            Assert.IsFalse(map.IsRegistGlobal(valid));
            Assert.IsTrue(ok);
            ok = map.UnRegistGlobalRule(valid);
            Assert.IsFalse(ok);

            map.RegistWithKeyRule("a", valid);
            Assert.IsTrue(map.IsRegistdWithKeyRule("a", valid));
            Assert.IsFalse(map.IsRegistdWithKeyRule("b", valid));
            ok = map.UnRegistWithKeyRule("a", valid);
            Assert.IsFalse(map.IsRegistdWithKeyRule("a", valid));
            Assert.IsTrue(ok);
            ok = map.UnRegistWithKeyRule("a", valid);
            Assert.IsFalse(ok);
        }
        [TestMethod]
        public void NoneRules_ValidatePass()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            map.AddOrUpdate("a", VarValue.Char0Value);
            Assert.AreEqual(VarValue.Char0Value, map["a"]);
        }
        [TestMethod]
        public void GlobalRule_SkipKeyRules()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            valid.IsSkipWithKeyValidate = true;
            map.RegistGlobalRule(valid);
            var keyValid = new ValueNotifyableSetValidater
            {
                Func = _ => false
            };
            map.RegistWithKeyRule("a", keyValid);

            map.AddOrUpdate("a", VarValue.Char0Value);

            Assert.AreEqual(VarValue.Char0Value, map["a"]);
        }
        [TestMethod]
        public void GlobalRule_StopRules()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            valid.IsStopValidate = true;

            var valid2 = new ValueNotifyableSetValidater
            {
                Func = _ => false
            };
            map.RegistGlobalRule(valid);
            map.RegistGlobalRule(valid2);

            map.AddOrUpdate("a", VarValue.Byte0Value);
            Assert.AreEqual(VarValue.Byte0Value, map["a"]);
        }
        [TestMethod]
        public void GlobalRule_SkipGlobalRules()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            valid.IsSkipGlobalValidate = true;

            var valid2 = new ValueNotifyableSetValidater
            {
                Func = _ => false
            };
            map.RegistGlobalRule(valid);
            map.RegistGlobalRule(valid2);

            map.AddOrUpdate("a", VarValue.Byte0Value);
            Assert.AreEqual(VarValue.Byte0Value, map["a"]);
        }
        [TestMethod]
        public void Global_OnlyACanWrite()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            valid.Func = a => a.Key == "a";

            map.RegistGlobalRule(valid);

            map.AddOrUpdate("a", VarValue.Byte0Value);

            Assert.ThrowsException<InvalidOperationException>(() => map.AddOrUpdate("b", VarValue.Byte0Value));
        }
        [TestMethod]
        public void ValidData()
        {
            var map = new ValidNotifyableMap<string, VarValue>();
            var valid = new ValueNotifyableSetValidater();
            valid.Func = a => a.Key == "a";

            map.RegistGlobalRule(valid);

            var e = new DataChangedEventArgs<string, VarValue>("a", null, null, ChangeModes.Change);
            Assert.IsTrue(map.ValidateData(e, out _));
            var e1 = new DataChangedEventArgs<string, VarValue>("b", null, null, ChangeModes.Change);
            Assert.IsFalse(map.ValidateData(e1, out var v));
            Assert.AreEqual(valid, v);

            Assert.IsTrue(map.ValidateData("a", VarValue.Byte0Value, out v));
            Assert.IsNull(v);

            Assert.IsFalse(map.ValidateData("b", VarValue.Byte0Value, out v));
            Assert.AreEqual(valid,v);

        }
    }
}
