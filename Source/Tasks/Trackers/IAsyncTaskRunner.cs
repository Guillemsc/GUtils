#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Tasks.Runners
{
    /// <summary>
    /// Represents an interface for running tasks asyncronously,
    /// while still keeping control over their cancellation.
    /// </summary>
    public interface IAsyncTaskRunner
    {
        /// <summary>
        /// Runs an asynchronous task represented by the provided function.
        /// </summary>
        /// <param name="func">The function that represents the asynchronous task.</param>
        /// <returns>A task representing the execution of the provided function.</returns>
        Task Run(Func<CancellationToken, Task> func);

        void CancelForever();
        void CancelCurrent();
    }
}
