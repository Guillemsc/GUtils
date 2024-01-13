using System;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;
using GUtils.Tasks.Trackers;
using GUtilsGodot.Tasks.Extensions;

namespace GUtils.Tasks.Extensions
{
    public static class AsyncTaskRunnerDiExtensions
    {
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
                .FromInstance(new AsyncTaskRunner(AsyncTaskRunnerExtensions.PrintException))
                .WhenDispose(Dispose)
                .NonLazy();
        }
    }
}
