using System.Collections.Generic;
using GUtils.Di.Delegates;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;
using GUtils.Loadables;

namespace GUtils.Di.Contexts
{
    public interface IDiContext<out TResult>
    {
        IDiContext<TResult> AddSettingLoadable<T>(ILoadable<T> loadable);
        IDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> loadable);
        IDiContext<TResult> AddInstaller(IInstaller installer);
        IDiContext<TResult> AddInstaller(InstallDelegate installer);
        IDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers);

        IDisposable<TResult> Install();
    }
}
