using GUtils.Logging.Builders;

namespace GUtils.Logging.Loggables
{
    public interface ILoggable
    {
        void Log(ILogBuilder logBuilder);
    }
}
