using System.Threading;
using System.Threading.Tasks;

namespace GUtils.StackedStateMachines.Async
{
    public sealed class NopAsyncState : IAsyncState
    {
        public static readonly NopAsyncState Instance = new();
        
        NopAsyncState() {}

        public Task LoadAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public void Start() {}
        public Task DisposeAsync() => Task.CompletedTask;
    }
}