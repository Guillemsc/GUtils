using System;

namespace GUtils.Profiling.Steps
{
    public sealed class ProfilingStep
    {
        public string Name { get; }
        public TimeSpan Duration { get; }

        public ProfilingStep(
            string name,
            TimeSpan duration
            )
        {
            Name = name;
            Duration = duration;
        }
    }
}
