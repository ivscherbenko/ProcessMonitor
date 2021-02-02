using System.Diagnostics;

namespace ProcessMonitor.Tools.Extensions
{
    public static class ProcessExtensions
    {
        public static string ReflectInString(this Process process)
        {
            return $"Name: {process.ProcessName}, ID: {process.Id}"; 
        }
    }
}
