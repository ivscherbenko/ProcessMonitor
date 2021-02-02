using NLog;
using ProcessMonitor.Tools.Exeptions;
using ProcessMonitor.Tools.Extensions;
using System;

namespace ProcessMonitor.Tools
{
    public static class CommandLineParser
    {
        private static readonly ILogger Logger = typeof(CommandLineParser).CreateLogger();
        private const int Required = 3;
        public static CommandLineParameters Parameters { get; private set; }

        public static bool TryParse(string[] args)
        {
            if (args.Length < Required)
            {
                Logger.Warn($"Unexpected arguments count. Required: {Required}, Actual: {args.Length}");
                return false; 
            }

            try
            {
                Parameters = new CommandLineParameters
                {
                    ProcessName = args[0],
                    Timeout = TimeSpan.FromMinutes(int.Parse(args[1])),
                    PollingInterval = TimeSpan.FromMinutes(int.Parse(args[2]))
                };
                return true;
            }
            catch (Exception)
            {
                throw new InvalidArgumentExeption("Unexpected arguments format");
            }
        }
    }
}
