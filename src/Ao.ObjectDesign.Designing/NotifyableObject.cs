﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    public delegate void PropertyChangeToEventHandler(object sender, PropertyChangeToEventArgs e);
    [Serializable]
    public class NotifyableObject : INotifyPropertyChanged, INotifyPropertyChanging, INotifyPropertyChangeTo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangeToEventHandler PropertyChangeTo;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanging([CallerMemberName] string name = null)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(name));
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChangeTo(object from, object to, [CallerMemberName] string name = null)
        {
            if (PropertyChangeTo != null)
            {
                PropertyChangeTo.Invoke(this, new PropertyChangeToEventArgs(name, from, to));
            }
        }
        protected void Set<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                T origin = prop;
                RaisePropertyChanging(name);
                prop = value;
                RaisePropertyChanged(name);
                RaisePropertyChangeTo(origin, value, name);
            }
        }
    }
}
