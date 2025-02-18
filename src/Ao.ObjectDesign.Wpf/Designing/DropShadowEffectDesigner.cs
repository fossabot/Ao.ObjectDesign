﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(DropShadowEffect))]
    public class DropShadowEffectDesigner : NotifyableObject
    {
        private double shadowDepth;
        private ColorDesigner color;
        private double direction;
        private double opacity;
        private double blurRadius;
        private RenderingBias renderingBias;

        [PlatformTargetProperty]
        public virtual DropShadowEffect DropShadowEffect
        {
            get => new DropShadowEffect
            {
                ShadowDepth = shadowDepth,
                Color = color?.Color ?? Colors.Transparent,
                BlurRadius = blurRadius,
                Opacity = opacity,
                Direction = direction,
                RenderingBias = renderingBias
            };
            set
            {
                if (value is null)
                {
                    ShadowDepth = Direction = BlurRadius = 0;
                    RenderingBias = RenderingBias.Performance;
                    Color = null;
                    Opacity = 0;
                }
                else
                {
                    ShadowDepth = value.ShadowDepth;
                    Direction = value.Direction;
                    BlurRadius = value.BlurRadius;
                    if (color is null)
                    {
                        Color = new ColorDesigner { Color = value.Color };
                    }
                    else
                    {
                        color.Color = value.Color;
                    }
                    RenderingBias = value.RenderingBias;
                    Opacity = value.Opacity;
                }
            }
        }
        [DefaultValue(RenderingBias.Performance)]
        public virtual RenderingBias RenderingBias
        {
            get => renderingBias;
            set
            {
                Set(ref renderingBias, value);
            }
        }
        [DefaultValue(0d)]
        public virtual double BlurRadius
        {
            get => blurRadius;
            set
            {
                Set(ref blurRadius, value);
            }
        }
        [DefaultValue(0d)]
        public virtual double Opacity
        {
            get => opacity;
            set
            {
                Set(ref opacity, value);
            }
        }
        [DefaultValue(0d)]
        public virtual double Direction
        {
            get => direction;
            set
            {
                Set(ref direction, value);
            }
        }
        [DefaultValue(null)]
        public virtual ColorDesigner Color
        {
            get => color;
            set
            {
                Set(ref color, value);
            }
        }
        [DefaultValue(0d)]
        public virtual double ShadowDepth
        {
            get => shadowDepth;
            set
            {
                Set(ref shadowDepth, value);
            }
        }
    }
}
