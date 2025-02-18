﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(RotateTransform))]
    public class RotateTransformDesigner : NotifyableObject
    {
        private double angle;
        private double centerX;
        private double centerY;

        [PlatformTargetProperty]
        public virtual RotateTransform RotateTransform
        {
            get => new RotateTransform(angle, centerX, centerY);
            set
            {
                if (value is null)
                {
                    angle = centerY = centerX = 0;
                }
                else
                {
                    Angle = value.Angle;
                    CenterX = value.CenterX;
                    CenterY = value.CenterY;
                }
            }
        }

        [DefaultValue(0d)]
        public virtual double CenterX
        {
            get => centerX;
            set
            {
                Set(ref centerX, value);
                RaiseRotateTransformChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double CenterY
        {
            get => centerY;
            set
            {
                Set(ref centerY, value);
                RaiseRotateTransformChanged();
            }
        }


        [DefaultValue(0d)]
        public virtual double Angle
        {
            get => angle;
            set
            {
                Set(ref angle, value);
                RaiseRotateTransformChanged();
            }
        }

        protected void RaiseRotateTransformChanged()
        {
            RaisePropertyChanged(nameof(RotateTransform));
        }
    }
}
