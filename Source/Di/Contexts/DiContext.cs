using System;
using System.Collections.Generic;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using GUtils.Di.Delegates;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;
using GUtils.Loadables;

namespace GUtils.Di.Contexts
{
    public sealed class DiContext<TResult> : IDiContext<TResult>
    {
        readonly List<Action<List<IInstaller>, List<IDisposable>>> _loadableSettings = new();
        readonly List<ILoadable<IInstaller>> _loadableInstallers = new();
        readonly List<IInstaller> _installers = new();

        bool _hasValidContainer;
        IDiContainer? _container;

        public IDiContext<TResult> AddSettingLoadable<T>(ILoadable<T> loadable)
        {
            void CreateInstaller(List<IInstaller> installers, List<IDisposable> disposables)
            {
                var value = loadable.Load();

                installers.Add(new CallbackInstaller(b => b.AddSettings(value.Value)));
                disposables.Add(value);
            }

            _loadableSettings.Add(CreateInstaller);
            return this;
        }

        public IDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> loadable)
        {
            _loadableInstallers.Add(loadable);
            return this;
        }

        public IDiContext<TResult> AddInstaller(IInstaller installer)
        {
            _installers.Add(installer);
            return this;
        }

        public IDiContext<TResult> AddInstaller(InstallDelegate installer)
        {
            _installers.Add(new CallbackInstaller(installer.Invoke));
            return this;
        }

        public IDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers)
        {
            _installers.AddRange(installers);
            return this;
        }

        public IDisposable<TResult> Install()
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            List<IDisposable> disposables = new();

            foreach (var installer in _loadableInstallers)
            {
                IDisposable<IInstaller> disposable = installer.Load();
                _installers.Add(disposable.Value);
                disposables.Add(disposable);
            }

            foreach (var loadableSetting in _loadableSettings)
            {
                loadableSetting.Invoke(_installers, disposables);
            }

            builder.Install(_installers);

            _container = builder.Build();

            TResult result = _container!.Resolve<TResult>();

            _hasValidContainer = true;

            return new CallbackDisposable<TResult>(
                result,
                x =>
                {
                    _hasValidContainer = false;
                    _container.Dispose();

                    foreach (var disposable in disposables)
                    {
                        disposable.Dispose();
                    }
                }
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
