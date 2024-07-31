using System.Threading;
using System.Threading.Tasks;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;

namespace GUtils.Loadables
{
    public sealed class InstallLoadedResultAdapterAsyncLoadable<T> : IAsyncLoadable<IInstaller>
    {
        readonly IAsyncLoadable<T> _asyncLoadable;

        public InstallLoadedResultAdapterAsyncLoadable(IAsyncLoadable<T> asyncLoadable)
        {
            _asyncLoadable = asyncLoadable;
        }
        
        public async Task<IAsyncDisposable<IInstaller>> Load(CancellationToken ct)
        {
            IAsyncDisposable<T> sourceResult = await _asyncLoadable.Load(ct);
            
            IInstaller installer = new CallbackInstaller(
                b => b.Bind<T>().FromInstance(sourceResult.Value)
            );
            
            IAsyncDisposable<IInstaller> asyncDisposable = new CallbackAsyncDisposable<IInstaller>(
                installer,
                _ => sourceResult.DisposeAsync()
            );
            
            return asyncDisposable;
        }
    }
}