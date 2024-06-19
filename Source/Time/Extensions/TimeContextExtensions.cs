using GUtils.Time.TimeContexts;
using GUtils.Time.Timers;

namespace GUtils.Time.Extensions
{
    public static class TimeContextExtensions
    {
        public static ITimer NewTimer(this ITimeContext timeContext)
        {
            return new TimeSourceTimer(timeContext);
        }
    
        public static ITimer NewStartedTimer(this ITimeContext timeContext)
        {
            ITimer timer = new TimeSourceTimer(timeContext);
            timer.Start();
            return timer;
        }
    }
}