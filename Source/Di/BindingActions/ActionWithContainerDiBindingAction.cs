using System;
using GUtils.Di.Container;

namespace GUtils.Di.BindingActions
{
    public sealed class ActionWithContainerDiBindingAction : IDiBindingAction
    {
        readonly Action<IDiResolveContainer, object> _action;

        public ActionWithContainerDiBindingAction(Action<IDiResolveContainer, object> action)
        {
            _action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            _action?.Invoke(resolver, obj);
        }
    }
}
