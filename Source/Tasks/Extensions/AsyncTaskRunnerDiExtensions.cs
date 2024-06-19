using GUtils.Di.Builder;
using GUtils.Tasks.Runners;
using GUtils.Tasks.Trackers;

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
                .FromInstance(new AsyncTaskRunner(default))
                .WhenDispose(Dispose)
                .NonLazy();
        }
    }
}
