using GUtils.Loading.Contexts;
using GUtils.StackedStateMachines.Async;

namespace GUtils.Source.StackedStateMachines.Extensions
{
    public static class StackedAsyncStateMachineLoadingContextExtensions
    {
        public static ILoadingContext EnqueuePush(
            this ILoadingContext loadingContext,
            StackedAsyncStateMachine stackedAsyncStateMachine,
            IAsyncState asyncState
        )
        {
            loadingContext
                .Enqueue(ct => stackedAsyncStateMachine.Push(asyncState, false))
                .EnqueueAfterLoad(asyncState.Start);

            return loadingContext;
        }

        public static ILoadingContext EnqueuePop<T>(
            this ILoadingContext loadingContext,
            StackedAsyncStateMachine stackedAsyncStateMachine
        ) where T : IAsyncState
        {
            loadingContext.Enqueue(ct => stackedAsyncStateMachine.Pop<T>());

            return loadingContext;
        }
    }
}