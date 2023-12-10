using System;

namespace GUtils.Disposing.Disposables
{
    public interface IAsyncDisposable<out T> : IAsyncDisposable
    {
        public T Value { get; }
    }
}
