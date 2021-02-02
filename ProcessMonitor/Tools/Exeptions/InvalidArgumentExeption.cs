using System;

namespace ProcessMonitor.Tools.Exeptions
{
    class InvalidArgumentExeption : Exception
    {
        public InvalidArgumentExeption()
        {
        }

        public InvalidArgumentExeption(string message) : base(message)
        {
        }
    }
}
