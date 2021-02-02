using NLog;
using ProcessMonitor.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessMonitor.Tools
{
    public class ProcessManager
    {
        private readonly string _processName;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval; 
        private static readonly ILogger Logger = typeof(ProcessManager).CreateLogger();

        public ProcessManager(CommandLineParameters parameters)
        {
            _processName = parameters.ProcessName;
            _timeout = parameters.Timeout;
            _pollingInterval = parameters.PollingInterval; 
        }

        public List<Process> Processes
        {
            get
            {
                var processes = Process.GetProcesses()
                    .ToList()
                    .FindAll(p => p.ProcessName.ContainsCaseInsensitive(_processName));
                Logger.Info($"Found processes: {processes.Count}");
                return processes;
            }
        }

        private void Watch(Process process, CancellationToken token)
        {
            while (true)
            {
                if (process.HasExited || token.IsCancellationRequested)
                {
                    Logger.Info("The process doesn't exist anymore");
                    break;
                }

                Logger.Info($"Waiting for process {process.ReflectInString()}");
                Thread.Sleep(_pollingInterval);
            }
        }

        public void KillProcessAfterTimeout()
        {
            var cancelTokenSource = new CancellationTokenSource();
            var token = cancelTokenSource.Token;
            var tasks = new Task[Processes.Count];

            for (var i = 0; i < Processes.Count; i++)
            {
                var j = i;
                tasks[i] = Task.Factory.StartNew(() => { Watch(Processes[j], token); }, token);
            }

            Task.WaitAll(tasks, _timeout);
            cancelTokenSource.Cancel(); 

            KillProcess(Processes);
        }

        private void KillProcess(List<Process> processes)
        {
            processes.ForEach(KillProcess);
        }

        private void KillProcess(Process process)
        {
            Logger.Info($"Killing the process: {process.ReflectInString()}");
            process.Kill();
            Logger.Info($"Waiting for the process to finish: {process.ReflectInString()}");
            process.WaitForExit();
            Logger.Info($"Process killed: {process.HasExited}");
        }
    }
}
