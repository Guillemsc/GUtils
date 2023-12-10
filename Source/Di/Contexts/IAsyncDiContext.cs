using System.Collections.Generic;
using System.Threading.Tasks;
using GUtils.Di.Container;
using GUtils.Di.Delegates;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;
using GUtils.Loadables;

namespace GUtils.Di.Contexts
{
    public interface IAsyncDiContext<TResult>
    {
        IAsyncDiContext<TResult> AddInstallerAsyncLoadable(IAsyncLoadable<IInstaller> asyncLoadable);
        IAsyncDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> asyncLoadable);
        IAsyncDiContext<TResult> AddInstaller(IInstaller installer);
        IAsyncDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers);
        IAsyncDiContext<TResult> AddInstaller(InstallDelegate installer);

        Task<ITaskDisposable<TResult>> Install();

        IDiContainer? GetContainerUnsafe();
    }
}
