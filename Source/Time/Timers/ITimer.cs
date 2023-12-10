using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Time.Timers
{
    /// <summary>
    /// Represents a generic timer that tracks elapsed time and provides various timer operations.
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// Gets a value indicating whether the timer has started.
        /// </summary>
        bool Started { get; }

        /// <summary>
        /// Gets a value indicating whether the timer is paused.
        /// </summary>
        bool Paused { get; }

        /// <summary>
        /// Gets a value indicating whether the timer has started and is not paused.
        /// </summary>
        bool StartedAndNotPaused { get; }

        /// <summary>
        /// Gets the current elapsed time of the timer.
        /// </summary>
        TimeSpan Time { get; }

        /// <summary>
        /// Starts the timer if it hadn't been started.
        /// </summary>
        void Start();

        /// <summary>
        /// Pauses the timer if had been started.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the timer if it was paused.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stops the timer and resets all its values.
        /// </summary>
        void Reset();

        /// <summary>
        /// Resets and starts again the timer.
        /// </summary>
        void Restart();

        /// <summary>
        /// Adds some time to the timer.
        /// </summary>
        void Add(TimeSpan timeSpan);

        /// <summary>
        /// Returns if the total time of the timer has reached a certain value.
        /// </summary>
        bool HasReached(TimeSpan time);
    }
}
