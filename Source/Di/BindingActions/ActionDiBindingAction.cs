using System;
using GUtils.Di.Container;

namespace GUtils.Di.BindingActions
{
    public sealed class ActionDiBindingAction : IDiBindingAction
    {
        readonly Action _action;

        public ActionDiBindingAction(Action action)
        {
            _action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            _action?.Invoke();
        }
    }
}
