using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Disposing.Disposables;

namespace GUtils.Disposing.Container
{
    public sealed class AsyncDisposablesContainer : IAddOnlyAsyncDisposablesContainer, IAsyncDisposable
    {
        readonly List<Func<Task>> _disposeActions = new();
        
        public void Add(Action dispose)
        {
            Add(() =>
            {
                dispose.Invoke();
                return Task.CompletedTask;
            });
        }

        public void Add(IDisposable disposable)
        {
            Add(() =>
            {
                disposable.Dispose();
                return Task.CompletedTask;
            });
        }

        public void Add(Func<Task> dispose)
        {
            _disposeActions.Add(dispose);
        }

        public void Add(ITaskDisposable disposable)
        {
            Add(disposable.DisposeAsync);
        }

        public void Add(IAsyncDisposable disposable)
        {
            Add(() => disposable.DisposeAsync());
        }
        
        public async ValueTask DisposeAsync()
        {
            foreach (Func<Task> entry in _disposeActions)
            {
                await entry.Invoke();
            }
            
            _disposeActions.Clear();
        }
    }
}