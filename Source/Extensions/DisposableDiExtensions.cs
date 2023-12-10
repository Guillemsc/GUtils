using System;
using GUtils.Di.Builder;

namespace GUtils.Extensions
{
    public static class DisposableDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkDisposable<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : IDisposable
        {
            return actionBuilder.WhenDispose(o => o.Dispose);
        }
    }
}
