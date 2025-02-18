﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(CornerRadius))]
    public class CornerRadiusDesigner : NotifyableObject
    {
        private double left;
        private double top;
        private double right;
        private double bottom;

        [DefaultValue(0d)]
        public virtual double Left
        {
            get => left;
            set
            {
                Set(ref left, value);
                RaiseCornerRadiusChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Top
        {
            get => top;
            set
            {
                Set(ref top, value);
                RaiseCornerRadiusChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Right
        {
            get => right;
            set
            {
                Set(ref right, value);
                RaiseCornerRadiusChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Bottom
        {
            get => bottom;
            set
            {
                Set(ref bottom, value);
                RaiseCornerRadiusChanged();
            }
        }

        [PlatformTargetProperty]
        public virtual CornerRadius CornerRadius
        {
            get => new CornerRadius(left, top, right, bottom);
            set
            {
                Left = value.TopLeft;
                Top = value.TopRight;
                Right = value.BottomRight;
                Bottom = value.BottomLeft;
                RaiseCornerRadiusChanged();
            }
        }
        protected void RaiseCornerRadiusChanged()
        {
            RaisePropertyChanged(nameof(CornerRadius));
        }
    }
}
