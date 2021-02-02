using NLog;
using System;

namespace ProcessMonitor.Tools.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger CreateLogger(this Type type)
        {
            var logger = LogManager.GetLogger(type.Name);
            return logger;
        }
    }
}
