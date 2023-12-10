using System.Threading;
using System.Threading.Tasks;
using GUtils.Disposing.Disposables;

namespace GUtils.Loadables
{
    public interface IAsyncLoadable<T>
    {
        Task<IAsyncDisposable<T>> Load(CancellationToken ct);
    }
}
