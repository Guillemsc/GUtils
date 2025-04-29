#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Di.Container;
using GUtils.Di.Delegates;
using GUtils.Di.Extensions;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;
using GUtils.Loadables;

namespace GUtils.Di.Contexts
{
    public sealed class AsyncDiContext<TResult> : IAsyncDiContext<TResult>
    {
        readonly List<IInstaller> _installers = new();
        readonly List<IAsyncLoadable<IInstaller>> _asyncInstallerLoadables = new();
        readonly List<ILoadable<IInstaller>> _installerLoadables = new();

        bool _hasValidContainer;
        IDiContainer? _container;

        public IAsyncDiContext<TResult> AddAsyncLoadable<TLoad>(IAsyncLoadable<TLoad> asyncLoadable)
        {
            _asyncInstallerLoadables.Add(new InstallLoadedResultAdapterAsyncLoadable<TLoad>(asyncLoadable));
            return this;
        }

        public IAsyncDiContext<TResult> AddInstallerAsyncLoadable(IAsyncLoadable<IInstaller> asyncLoadable)
        {
            _asyncInstallerLoadables.Add(asyncLoadable);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> asyncLoadable)
        {
            _installerLoadables.Add(asyncLoadable);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstaller(IInstaller installer)
        {
            _installers.Add(installer);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers)
        {
            _installers.AddRange(installers);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstaller(InstallDelegate installer)
        {
            _installers.Add(new CallbackInstaller(installer.Invoke));
            return this;
        }

        public async Task<ITaskDisposable<TResult>> InstallAsync()
        {
            List<IInstaller> allInstallers = new(_installers);
            List<IAsyncDisposable> asyncDisposables = new();
            List<IDisposable> syncDisposables = new();
            
            foreach (var asyncLoadable in _asyncInstallerLoadables)
            {
                IAsyncDisposable<IInstaller> asyncDisposable = await asyncLoadable.Load(CancellationToken.None);
                asyncDisposables.Add(asyncDisposable);
                allInstallers.Add(asyncDisposable.Value);
            }

            foreach (var loadable in _installerLoadables)
            {
                IDisposable<IInstaller> installer = loadable.Load();
                syncDisposables.Add(installer);
                allInstallers.Add(installer.Value);
            }

            _container = DiContainerBuilderExtensions.BuildFromInstallers(allInstallers);

            async Task Dispose(TResult result)
            {
                _hasValidContainer = false;

                _container.Dispose();

                foreach (var asyncDisposable in asyncDisposables)
                {
                    await asyncDisposable.DisposeAsync();
                }

                foreach (var disposable in syncDisposables)
                {
                    disposable.Dispose();
                }
            }

            TResult result = _container!.Resolve<TResult>();

            _hasValidContainer = true;

            return new CallbackTaskDisposable<TResult>(
                result,
                Dispose
            );
        }

        public IDiContainer GetContainerUnsafe()
        {
            if (!_hasValidContainer)
            {
                throw new AccessViolationException("Tried to get container but it was not created or already disposed");
            }

            return _container!;
        }
    }
}
