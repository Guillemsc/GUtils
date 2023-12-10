using GUtils.Logging.Enums;

namespace GUtils.Logging.Outputs
{
    public interface ILogOutput
    {
        void Output(LogType logType, string log);
    }
}
