﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontWeight))]
    public class FontWeightDesigner : NotifyableObject
    {
        public static readonly IReadOnlyDictionary<PenFontWeights, FontWeight> fontWeightMap = new Dictionary<PenFontWeights, FontWeight>
        {
            [PenFontWeights.Black] = FontWeights.Black,
            [PenFontWeights.Bold] = FontWeights.Bold,
            [PenFontWeights.DemiBold] = FontWeights.DemiBold,
            [PenFontWeights.ExtraBlack] = FontWeights.ExtraBlack,
            [PenFontWeights.ExtraBold] = FontWeights.ExtraBold,
            [PenFontWeights.ExtraLight] = FontWeights.ExtraLight,
            [PenFontWeights.Heavy] = FontWeights.Heavy,
            [PenFontWeights.Light] = FontWeights.Light,
            [PenFontWeights.Medium] = FontWeights.Medium,
            [PenFontWeights.Normal] = FontWeights.Normal,
            [PenFontWeights.Regular] = FontWeights.Regular,
            [PenFontWeights.SemiBold] = FontWeights.SemiBold,
            [PenFontWeights.Thin] = FontWeights.Thin,
            [PenFontWeights.UltraBlack] = FontWeights.UltraBlack,
            [PenFontWeights.UltraBold] = FontWeights.UltraBold,
            [PenFontWeights.UltraLight] = FontWeights.UltraLight,
        };
        public static readonly IReadOnlyList<PenFontWeights> KnowPenFontWeights = Enum.GetValues(typeof(PenFontWeights)).OfType<PenFontWeights>().ToArray();

        private static PenFontWeights GetFontWeight(FontWeight fontWeight)
        {
            return fontWeightMap.FirstOrDefault(x => x.Value == fontWeight)
                .Key;
        }

        private bool isBold;
        private PenFontWeights penFontWeight;

        [PlatformTargetProperty]
        public virtual FontWeight FontWeight
        {
            get
            {
                if (fontWeightMap.TryGetValue(PenFontWeight, out FontWeight w))
                {
                    return w;
                }
                return FontWeights.Normal;
            }
            set
            {
                PenFontWeight = GetFontWeight(value);
                IsBold = PenFontWeight == PenFontWeights.Bold;
            }

        }


        [DefaultValue(false)]
        public bool IsBold
        {
            get => isBold;
            set
            {
                Set(ref isBold, value);
                PenFontWeight = value ? PenFontWeights.Bold : PenFontWeights.Normal;
            }
        }
        [DefaultValue(PenFontWeights.Normal)]
        public PenFontWeights PenFontWeight
        {
            get => penFontWeight;
            set
            {
                Set(ref penFontWeight, value);
                RaisePropertyChanged(nameof(FontWeight));
            }
        }
    }
}
