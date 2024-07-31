using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace GUtils.StackedStateMachines.Async
{
    public interface IAsyncState : ILoadableAsync, IStartable, ITaskDisposable
    {
        
    }
}