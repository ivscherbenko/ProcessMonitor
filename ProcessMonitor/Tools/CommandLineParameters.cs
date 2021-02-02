using System;

namespace ProcessMonitor.Tools
{
    public class CommandLineParameters
    {
        public string ProcessName { get; set; }

        public TimeSpan Timeout { get; set; }

        public TimeSpan PollingInterval { get; set; }
    }
}