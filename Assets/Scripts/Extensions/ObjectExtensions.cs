﻿using System.Collections.Generic;

namespace Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> ToDictionary(this object target)
        {
            return (Dictionary<string, object>) target;
        }
    }
}