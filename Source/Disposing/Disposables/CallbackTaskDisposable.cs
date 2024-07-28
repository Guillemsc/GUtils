using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Disposing.Disposables
{
    public sealed class CallbackTaskDisposable<T> : ITaskDisposable<T>
    {
        readonly Func<T, CancellationToken, Task> _onDispose;

        public T Value { get; }

        public CallbackTaskDisposable(T value, Func<T, CancellationToken, Task> onDispose)
        {
            Value = value;
            _onDispose = onDispose;
        }

        public Task DisposeAsync(CancellationToken cancellationToken)
        {
            return _onDispose.Invoke(Value, cancellationToken);
        }
    }
}
