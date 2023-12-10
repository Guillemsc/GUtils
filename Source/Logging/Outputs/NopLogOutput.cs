using GUtils.Logging.Enums;

namespace GUtils.Logging.Outputs
{
    public sealed class NopLogOutput : ILogOutput
    {
        public static readonly NopLogOutput Instance = new();

        NopLogOutput() { }

        public void Output(LogType logType, string log) { }
    }
}
