using System;
using System.Collections.Generic;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using GUtils.Di.Delegates;
using GUtils.Di.Installers;

namespace GUtils.Di.Children
{
    [Obsolete]
    public sealed class DiChildContainer : IDiChildContainer, IDisposable
    {
        readonly DiContainerBuilder _childBuilder = new();

        IDiContainer? _childContainer;

        public IDiBindingBuilder<T> Bind<T>()
        {
            return _childBuilder.Bind<T>();
        }

        public IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>()
            where TConcrete : TInterface
        {
            return _childBuilder.Bind<TInterface, TConcrete>();
        }

        public IDiContainerBuilder Bind<T>(BindingBuilderDelegate<T> bindingBuilderDelegate)
        {
            return _childBuilder.Bind(bindingBuilderDelegate);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(IEnumerable<IInstaller> containers)
        {
            return _childBuilder.Install(containers);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(params IInstaller[] installers)
        {
            return _childBuilder.Install(installers);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(IReadOnlyList<IInstaller> container)
        {
            return _childBuilder.Install(container);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(Action<IDiContainerBuilder> installAction)
        {
            return _childBuilder.Install(installAction);
        }

        public IDiContainerBuilder Install(params IInstaller[] installers)
        {
            return _childBuilder.Install(installers);
        }

        public IDiContainerBuilder Install(IReadOnlyList<IInstaller> installers)
        {
            return _childBuilder.Install(installers);
        }

        public IDiContainerBuilder Install(Action<IDiContainerBuilder> action)
        {
            return _childBuilder.Install(action);
        }

        public IDiContainerBuilder WhenInit(Action action)
        {
            return _childBuilder.WhenInit(action);
        }

        public IDiContainerBuilder WhenInit(Action<IDiResolveContainer> action)
        {
            return _childBuilder.WhenInit(action);
        }

        public IDiContainerBuilder WhenDispose(Action action)
        {
            return _childBuilder.WhenDispose(action);
        }

        public IDiContainerBuilder WhenDispose(Action<IDiResolveContainer> action)
        {
            return _childBuilder.WhenDispose(action);
        }

        public T Resolve<T>()
        {
            if (_childContainer == null)
            {
                throw new Exception($"Tried to resolve {typeof(T).Name} before parent container was built");
            }

            return _childContainer.Resolve<T>();
        }

        public T Resolve<T>(object id)
        {
            throw new NotImplementedException();
        }

        public bool TryResolve<T>(out T value)
        {
            if (_childContainer == null)
            {
                throw new Exception($"Tried to resolve {typeof(T).Name} before parent container was built");
            }

            return _childContainer.TryResolve(out value);
        }

        public bool TryResolve<T>(object id, out T value)
        {
            throw new NotImplementedException();
        }

        public void WhenParentContainerBuild(IDiContainer parentContainer)
        {
            _childContainer = _childBuilder.Build(parentContainer);
        }

        public void Dispose()
        {
            _childContainer?.Dispose();
        }
    }
}
