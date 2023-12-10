using System;

namespace GUtils.Time.TimeSources
{
    /// <summary>
    /// Represents a time source, which starts at some point, and provides the time since it has started.
    /// </summary>
    public interface ITimeSource
    {
        /// <summary>
        /// Current time since the source has started.
        /// </summary>
        TimeSpan Time { get; }
    }
}
