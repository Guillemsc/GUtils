using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace GUtils.ApplicationContexts.Contexts
{
    public interface IApplicationContext : ILoadable, IStartable, ITaskDisposable
    {

    }
}