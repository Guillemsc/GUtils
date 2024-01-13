using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using GUtils.Tasks.Runners;

namespace GUtils.Tasks.Trackers
{
    /// <inheritdoc cref="IAsyncTaskRunner" />
    public sealed class AsyncTaskRunner : IAsyncTaskRunner, IDisposable
    {
        readonly Action<Exception>? _onException; 
        
        CancellationTokenSource _cancellationTokenSource = new();
        bool _hasRunAny;
        bool _isCanceledForever;

        public AsyncTaskRunner()
        {
            
        }

        public AsyncTaskRunner(Action<Exception> onException)
        {
            _onException = onException;
        }

        public Task Run(Func<CancellationToken, Task> func)
        {
            if (_isCanceledForever || _cancellationTokenSource.IsCancellationRequested)
            {
                return Task.FromCanceled(_cancellationTokenSource.Token);
            }

            _hasRunAny = true;

            var task = func.Invoke(_cancellationTokenSource.Token);
            task.RunAsync(e =>
            {
                if (_onException == null)
                {
                    throw e;   
                }
                
                _onException.Invoke(e);
            });
            return task;
        }

        public void CancelForever()
        {
            _isCanceledForever = true;
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        public void CancelCurrent()
        {
            bool canCancel = !_isCanceledForever &&
                             _hasRunAny &&
                             !_cancellationTokenSource.IsCancellationRequested;
            
            if (canCancel)
            {
                return;
            }

            _hasRunAny = false;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}
