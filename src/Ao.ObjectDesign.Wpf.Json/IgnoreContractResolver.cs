﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf.Json
{
    public class IgnoreContractResolver : DefaultContractResolver
    {
        public HashSet<Type> IgnoreTypes { get; } = new HashSet<Type>();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo info && IgnoreTypes.Contains(info.PropertyType))
            {
                prop.Ignored = true;
            }
            return prop;
        }
    }
}
