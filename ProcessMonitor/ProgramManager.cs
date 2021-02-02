using NLog;
using ProcessMonitor.Tools;
using ProcessMonitor.Tools.Extensions;
using System;
using System.Linq;

namespace ProcessMonitor
{
    public class ProgramManager
    {
        private static readonly ILogger Logger = typeof(ProgramManager).CreateLogger();
        private CommandLineParameters _parameters = new CommandLineParameters(); 

        public void StartMonitoring()
        {
            var manager = new ProcessManager(_parameters);
            var processes = manager.Processes;

            if (!processes.Any())
            {
                Logger.Info($"There is no process with name '{_parameters.ProcessName}'");
                return;
            }

            manager.KillProcessAfterTimeout();
        }

        public void GetArguments(string[] args)
        {
            var result = Executor.Try(() => CommandLineParser.TryParse(args));
            if (!result)
            {
                PrintHelp();
                Environment.Exit(1);
            }

            _parameters = CommandLineParser.Parameters; 

            Logger.Info($"Process name: {_parameters.ProcessName}");
            Logger.Info($"Timeout: {_parameters.Timeout}");
            Logger.Info($"Polling interval: {_parameters.PollingInterval}");
        }

        private static void PrintHelp()
        {
            Logger.Info(@"Example: 'notepad 5 1'");
            Logger.Info(@"Argument 1[required, string]: processname");
            Logger.Info(@"Argument 2[required, int]: timeout (minutes)");
            Logger.Info(@"Argument 3[required, int]: polling interval (minutes)");
        }
    }
}
