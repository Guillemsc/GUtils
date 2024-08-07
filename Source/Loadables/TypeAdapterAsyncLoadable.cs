using System.Threading;
using System.Threading.Tasks;
using GUtils.Disposing.Disposables;

namespace GUtils.Loadables
{
    public sealed class TypeAdapterAsyncLoadable<TSource, TDestiny> : IAsyncLoadable<TDestiny>
        where TSource : TDestiny
    {
        readonly IAsyncLoadable<TSource> _asyncLoadable;

        public TypeAdapterAsyncLoadable(IAsyncLoadable<TSource> asyncLoadable)
        {
            _asyncLoadable = asyncLoadable;
        }

        public async Task<IAsyncDisposable<TDestiny>> Load(CancellationToken ct)
        {
            IAsyncDisposable<TSource> sourceResult = await _asyncLoadable.Load(ct);
            return new TypeAdapterAsyncDisposable<TSource, TDestiny>(sourceResult);
        }
    }
}
