﻿using Ao.ObjectDesign.Designing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IWpfBindingDrawingItem : IBindingDrawingItem
    {
        DependencyProperty DependencyProperty { get; }
    }
}
