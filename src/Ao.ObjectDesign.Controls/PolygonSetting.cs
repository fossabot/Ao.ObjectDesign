﻿using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Polygon))]
    public class PolygonSetting : ShapeSetting, IMiddlewareDesigner<Polygon>
    {
        private PointCollectionDesigner points;
        private FillRule fillRule;

        [DefaultValue(FillRule.EvenOdd)]
        public virtual FillRule FillRule
        {
            get => fillRule;
            set => Set(ref fillRule, value);
        }

        public virtual PointCollectionDesigner Points
        {
            get => points;
            set => Set(ref points, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            Points = new PointCollectionDesigner();
            FillRule = FillRule.EvenOdd;
        }

        public void Apply(Polygon value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Shape)value);
                if (value.Points is null)
                {
                    Points = new PointCollectionDesigner();
                }
                else
                {
                    Points = new PointCollectionDesigner { PointCollection = value.Points };
                }
                FillRule = value.FillRule;
            }
        }

        public void WriteTo(Polygon value)
        {
            if (value != null)
            {
                WriteTo((Shape)value);
                value.Points = points?.PointCollection;
                value.FillRule = fillRule;
            }
        }
    }
}
