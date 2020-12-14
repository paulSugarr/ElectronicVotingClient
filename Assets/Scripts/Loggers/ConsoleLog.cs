using System;
using UnityEngine;

namespace Loggers
{
    public static class ConsoleLog
    {
        private static readonly Action<string> Logger = Debug.Log;
        
        public static void Print(this object source, string message)
        {
            var log = $"{source}: " + message;
            Logger.Invoke(log);
        }
    }
}