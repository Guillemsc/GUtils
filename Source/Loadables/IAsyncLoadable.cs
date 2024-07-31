using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Disposing.Disposables;

namespace GUtils.Loadables
{
    public interface IAsyncLoadable
    {
        Task<IAsyncDisposable> Load(CancellationToken ct);
    }
    
    public interface IAsyncLoadable<T>
    {
        Task<IAsyncDisposable<T>> Load(CancellationToken ct);
    }
}
