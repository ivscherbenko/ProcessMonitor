namespace ProcessMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new ProgramManager(); 
            manager.GetArguments(args);
            manager.StartMonitoring(); 
        }
    }
}
