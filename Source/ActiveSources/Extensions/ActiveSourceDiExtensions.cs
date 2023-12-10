using System;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using GUtils.ActiveSources;

namespace GUtils.ActiveSources.Extensions
{
    public static class ActiveSourceDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkSingleActiveSourceStateChange<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<T, ISingleActiveSource> getActiveSource,
            Func<IDiResolveContainer, Action<bool>> getChangedAction
        )
        {
            ISingleActiveSource activeSource = null!;
            Action<bool> changedAction = null!;

            actionBuilder.WhenInit((c, o) =>
            {
                activeSource = getActiveSource.Invoke(o);
                changedAction = getChangedAction.Invoke(c);

                activeSource.OnActiveChanged += changedAction;
            });

            actionBuilder.WhenDispose(() =>
            {
                activeSource.OnActiveChanged -= changedAction;
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
