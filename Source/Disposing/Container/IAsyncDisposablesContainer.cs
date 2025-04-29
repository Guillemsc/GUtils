using System;
using System.Threading.Tasks;
using GUtils.Disposing.Disposables;

namespace GUtils.Disposing.Container
{
    public interface IAddOnlyAsyncDisposablesContainer
    {
        void Add(Action dispose);
        void Add(IDisposable disposable);
        void Add(Func<Task> dispose);
        void Add(ITaskDisposable disposable);
        void Add(IAsyncDisposable disposable);
    }
}