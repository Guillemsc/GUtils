using System;
using GUtils.Logging.Enums;

namespace GUtils.Logging.Outputs
{
    public sealed class ConsoleLogOutput : ILogOutput
    {
        public static readonly ConsoleLogOutput Instance = new();

        ConsoleLogOutput()
        {

        }

        public void Output(LogType logType, string log)
        {
            Console.WriteLine($"[{logType}] {log}");
        }
    }
}
