using GUtils.Di.Container;

namespace GUtils.Di.Delegates
{
    public delegate T BindingResolverDelegate<out T>(IDiResolveContainer resolveContainer);
}
