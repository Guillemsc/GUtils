using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Disposing.Disposables
{
    public sealed class CallbackTaskDisposable<T> : ITaskDisposable<T>
    {
        readonly Func<T, Task> _onDispose;

        public T Value { get; }

        public CallbackTaskDisposable(T value, Func<T, Task> onDispose)
        {
            Value = value;
            _onDispose = onDispose;
        }

        public Task DisposeAsync()
        {
            return _onDispose.Invoke(Value);
        }
    }
}
