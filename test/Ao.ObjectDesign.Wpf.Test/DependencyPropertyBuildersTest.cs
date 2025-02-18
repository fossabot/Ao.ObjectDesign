﻿using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class DependencyPropertyBuildersTest
    {
        class Student
        {
            public string Name { get; set; }
        }
        [TestMethod]
        public void BindWithExpression()
        {
            var t = new Thread(() =>
              {
                  var stu = new Student();
                  var dp = Button.NameProperty;

                  var btn = new Button();
                  var res = DependencyPropertyBuilders.Bind(dp, btn,
                      stu, x => x.Name);
                  var bd = BindingOperations.GetBinding(btn, dp);
                  Assert.IsNotNull(bd);
                  Assert.AreEqual(nameof(Student.Name), bd.Path.Path);
                  Assert.AreEqual(stu, bd.Source);
              });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
    }
}
