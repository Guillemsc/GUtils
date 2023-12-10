#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Events;

namespace GUtils.Tasks.Runners
{
    public interface IAsyncSequenceTaskRunner
    {
        IListenEvent<EventArgs> OnBeginRunning { get; }
        IListenEvent<EventArgs> OnEndRunning { get; }

        Task RunGetCompleteTask(Func<CancellationToken, Task> func);
        void Run(Func<CancellationToken, Task> func);
        void Cancel();
    }
}
