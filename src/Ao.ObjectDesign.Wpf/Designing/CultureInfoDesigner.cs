﻿using Ao.ObjectDesign.Wpf.Annotations;
using System.Globalization;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(CultureInfo))]
    public class CultureInfoDesigner : NotifyableObject
    {
        private string name;

        public virtual string Name
        {
            get => name;
            set
            {
                Set(ref name, value);
                RaisePropertyChanged(nameof(CultureInfo));
            }
        }

        [PlatformTargetProperty]
        public virtual CultureInfo CultureInfo
        {
            get => string.IsNullOrEmpty(name) ? null : new CultureInfo(name);
            set
            {
                if (value is null)
                {
                    Name = null;
                }
                else
                {
                    Name = value.Name;
                }
            }
        }
    }
}
