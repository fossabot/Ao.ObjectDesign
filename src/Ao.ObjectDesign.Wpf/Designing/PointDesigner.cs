﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Point))]
    public class PointDesigner : NotifyableObject
    {
        private double x;
        private double y;

        [DefaultValue(0d)]
        public virtual double X
        {
            get => x;
            set
            {
                Set(ref x, value);
                RaisePointChanged();
            }
        }
        [DefaultValue(0d)]
        public virtual double Y
        {
            get => y;
            set
            {
                Set(ref y, value);
                RaisePointChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual Point Point
        {
            get => new Point(x, y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        protected void RaisePointChanged()
        {
            RaisePropertyChanged(nameof(Point));
        }
    }
}
