using GUtils.Disposing.Disposables;

namespace GUtils.Loadables
{
    public interface ILoadable<out T>
    {
        IDisposable<T> Load();
    }
}
