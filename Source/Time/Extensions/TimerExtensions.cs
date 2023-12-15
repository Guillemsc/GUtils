using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Time.Timers;

namespace GUtils.Time.Extensions
{
    public static class TimerExtensions
    {
        /// <summary>
        /// Asynchronously waits until the total time of the timer reaches a certain value.
        /// </summary>
        /// <param name="time">The time span to wait for.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task AwaitReach(
            this ITimer timer,
            TimeSpan time,
            CancellationToken cancellationToken
        )
        {
            while (!timer.HasReached(time) && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Starts the timer and asynchronously waits until the total time of the timer reaches a certain value.
        /// </summary>
        /// <param name="time">The time span to wait for.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static Task StartAndAwaitReach(
            this ITimer timer, 
            TimeSpan time, 
            CancellationToken cancellationToken
            )
        {
            timer.Start();
            
            return AwaitReach(timer, time, cancellationToken);
        }

        /// <summary>
        /// Asynchronously waits for a certain time span from the moment the function is called.
        /// </summary>
        /// <param name="time">The time span to wait for.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static Task AwaitSpan(
            this ITimer timer,
            TimeSpan time,
            CancellationToken cancellationToken
        )
        {
            TimeSpan timeToReach = timer.Time + time;
            
            return timer.AwaitReach(timeToReach, cancellationToken);
        }

        /// <summary>
        /// Starts the timer and asynchronously waits for a certain time span from the moment the function is called.
        /// </summary>
        /// <param name="time">The time span to wait for.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static Task StartAndAwaitSpan(
            this ITimer timer,
            TimeSpan time,
            CancellationToken cancellationToken
        )
        {
            timer.Start();
            
            return timer.AwaitSpan(time, cancellationToken);
        }

        public static bool HasReachedOrNotStarted(this ITimer timer, TimeSpan timeSpan)
        {
            if (!timer.Started)
            {
                return true;
            }

            return timer.HasReached(timeSpan);
        }
    }
}
