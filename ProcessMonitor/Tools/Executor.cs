using NLog;
using ProcessMonitor.Tools.Extensions;
using System;

namespace ProcessMonitor.Tools
{
    public static class Executor
    {
        private static readonly ILogger Logger = typeof(Executor).CreateLogger(); 

        public static bool Try(Func<bool> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                Logger.Warn(e);
                return false;
            }
        }
    }
}
