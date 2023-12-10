using GUtils.Di.Container;

namespace GUtils.Di.BindingActions
{
    public interface IDiBindingAction
    {
        void Execute(IDiResolveContainer resolver, object obj);
    }
}
