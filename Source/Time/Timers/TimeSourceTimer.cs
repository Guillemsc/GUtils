using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Time.TimeSources;

namespace GUtils.Time.Timers
{
    /// <inheritdoc />
    /// <summary>
    /// Generic implementation of a timer, with exception of the TimeSource.
    /// </summary>
    public class TimeSourceTimer : ITimer
    {
        readonly ITimeSource _timeSource;

        TimeSpan _startTime;
        TimeSpan _pausedTime;

        public bool Started { get; private set; }
        public bool Paused { get; private set; }
        public bool StartedAndNotPaused => Started && !Paused;

        public TimeSpan Time
        {
            get
            {
                if (!Started)
                {
                    return TimeSpan.Zero;
                }

                if (Paused)
                {
                    return _pausedTime;
                }

                return _timeSource.Time - _startTime;
            }
        }

        public TimeSourceTimer(ITimeSource timeSource)
        {
            _timeSource = timeSource;
        }

        public void Start()
        {
            if (Started)
            {
                return;
            }

            if (Paused)
            {
                return;
            }

            Started = true;

            _startTime = _timeSource.Time;
        }

        public void Pause()
        {
            if (!Started)
            {
                return;
            }

            if (Paused)
            {
                return;
            }

            _pausedTime = Time;

            Paused = true;
        }

        public void Resume()
        {
            if (!Started)
            {
                return;
            }

            if (!Paused)
            {
                return;
            }

            Paused = false;

            _startTime = _timeSource.Time - _pausedTime;
        }

        public void Reset()
        {
            Started = false;
            Paused = false;

            _startTime = TimeSpan.Zero;
            _pausedTime = TimeSpan.Zero;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public void Add(TimeSpan timeSpan)
        {
            if (!Started)
            {
                return;
            }

            if (Paused)
            {
                _pausedTime += timeSpan;
            }
            else
            {
                _startTime -= timeSpan;   
            }
        }

        public bool HasReached(TimeSpan timeSpan)
        {
            if (!Started)
            {
                return false;
            }

            return TimeSpan.Compare(timeSpan, Time) == -1;
        }
    }
}
