using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace GUtils.StateMachines.Async
{
    public interface IAsyncState : ILoadable, IStartable, ITaskDisposable
    {
        
    }
}