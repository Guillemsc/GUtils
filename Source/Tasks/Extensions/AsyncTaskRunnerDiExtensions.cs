using System;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;
using GUtils.Tasks.Trackers;

namespace GUtils.Tasks.Extensions
{
    public static class AsyncTaskRunnerDiExtensions
    {
        [Obsolete("Use InstallAsyncTaskRunner")]
        public static IDiBindingActionBuilder<AsyncTaskRunner> BindAsyncTaskRunner(
            this IDiContainerBuilder builder
        ) => InstallAsyncTaskRunner(builder);

        public static IDiBindingActionBuilder<AsyncTaskRunner> InstallAsyncTaskRunner(
            this IDiContainerBuilder builder
        )
        {
            void Dispose(AsyncTaskRunner asyncTaskRunner)
            {
                asyncTaskRunner.CancelForever();
                asyncTaskRunner.Dispose();
            }

            return builder.Bind<IAsyncTaskRunner, AsyncTaskRunner>()
                .FromInstance(new AsyncTaskRunner())
                .WhenDispose(Dispose)
                .NonLazy();
        }
    }
}
