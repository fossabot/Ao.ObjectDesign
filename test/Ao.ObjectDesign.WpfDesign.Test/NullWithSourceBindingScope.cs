﻿using Ao.ObjectDesign.Wpf.Data;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign.Test
{
    class NullWithSourceBindingScope : IWithSourceBindingScope
    {
        public BindingExpressionBase Bind(DependencyObject @object)
        {
            return null;
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return null;
        }
    }
}
