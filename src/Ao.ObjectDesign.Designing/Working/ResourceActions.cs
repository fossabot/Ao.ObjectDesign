﻿using System;

namespace Ao.ObjectDesign.Designing.Working
{
    [Flags]
    public enum ResourceActions
    {
        Unknow = 0,
        Created = 1,
        Removed = 2,
        Copied = 3,
    }
}
