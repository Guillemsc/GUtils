using System.Threading.Tasks;
using System;
using System.Threading;

namespace GUtils.Disposing.Disposables
{
    /// <summary>
    /// Same as <see cref="IDisposable"/> but the disposing is done asynchronously.
    /// </summary>
    public interface ITaskDisposable
    {
        Task Dispose(CancellationToken cancellationToken);
    }

    /// <summary>
    /// Same as <see cref="ITaskDisposable"/> but with an enforced type.
    /// </summary>
    public interface ITaskDisposable<out T> : ITaskDisposable
    {
        public T Value { get; }
    }
}
