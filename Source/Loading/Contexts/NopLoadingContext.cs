using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Loading.Contexts
{
    public sealed class NopLoadingContext : ILoadingContext
    {
        public static readonly NopLoadingContext Instance = new();

        NopLoadingContext()
        {

        }

        public ILoadingContext Enqueue(Func<CancellationToken, Task> function) => this;
        public ILoadingContext Enqueue(Action action) => this;
        public ILoadingContext EnqueueAfterLoad(params Action[] action) => this;
        public ILoadingContext RunBeforeLoadActionsInstantly() => this;
        public ILoadingContext DontRunAfterLoadActions() => this;

        public Task Execute(CancellationToken cancellationToken) => Task.CompletedTask;
        public void ExecuteAsync() { }
    }
}
