﻿using System.Windows;
using Ao.ObjectDesign.Designing;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfUISpirit : UISpirit<FrameworkElement, WpfForViewBuildContext>, IWpfUISpirit
    {
        public WpfUISpirit(WpfForViewBuildContext context) : base(context)
        {
        }

        public WpfUISpirit(FrameworkElement view, WpfForViewBuildContext context) : base(view, context)
        {
        }
    }
}
