using System;
using GUtils.Di.BindingActions;
using GUtils.Di.Bindings;
using GUtils.Di.Container;

namespace GUtils.Di.Builder
{
    public sealed class DiBindingActionBuilder<T> : IDiBindingActionBuilder<T>
    {
        readonly DiBinding _binding;

        public DiBindingActionBuilder(DiBinding binding)
        {
            _binding = binding;
        }

        public Type IdentifierType => _binding.IdentifierType;
        public Type ActualType => _binding.ActualType;

        public IDiBindingActionBuilder<T> NonLazy()
        {
            _binding.NonLazy();

            return this;
        }

        public IDiBindingActionBuilder<T> WhenInit(Action<IDiResolveContainer, T> action)
        {
            void CastedAction(IDiResolveContainer resolver, object obj) => action?.Invoke(resolver, (T)obj);

            IDiBindingAction bindingAction = new ActionWithContainerDiBindingAction(CastedAction);

            _binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenInit(Action<T> action)
        {
            void CastedAction(object obj) => action?.Invoke((T)obj);

            IDiBindingAction bindingAction = new ActionWithoutContainerDiBindingAction(CastedAction);

            _binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenInit(Action action)
        {
            IDiBindingAction bindingAction = new ActionDiBindingAction(action);

            _binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenInit(Func<T, Action> func)
        {
            void CastedAction(object obj)
            {
                Action returnedAction = func.Invoke((T)obj);

                returnedAction?.Invoke();
            }

            IDiBindingAction bindingAction = new ActionWithoutContainerDiBindingAction(CastedAction);

            _binding.AddInitAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(Action<IDiResolveContainer, T> action)
        {
            void CastedAction(IDiResolveContainer resolver, object obj) => action.Invoke(resolver, (T)obj);

            IDiBindingAction bindingAction = new ActionWithContainerDiBindingAction(CastedAction);

            _binding.AddDisposeAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(Action<T> action)
        {
            void CastedAction(object obj) => action.Invoke((T)obj);

            IDiBindingAction bindingAction = new ActionWithoutContainerDiBindingAction(CastedAction);

            _binding.AddDisposeAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(Action action)
        {
            IDiBindingAction bindingAction = new ActionDiBindingAction(action);

            _binding.AddDisposeAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(Func<T, Action> func)
        {
            void CastedAction(object obj)
            {
                Action returnedAction = func.Invoke((T)obj);

                returnedAction?.Invoke();
            }

            IDiBindingAction bindingAction = new ActionWithoutContainerDiBindingAction(CastedAction);

            _binding.AddDisposeAction(bindingAction);

            return this;
        }

        public IDiBindingActionBuilder<T> WhenDispose(IDisposable disposable)
        {
            IDiBindingAction bindingAction = new EmptyActionWithouthContainerDiBindingAction(disposable.Dispose);
            _binding.AddDisposeAction(bindingAction);

            return this;
        }
    }
}
