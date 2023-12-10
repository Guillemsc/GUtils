using GUtils.Loading.Services;
using GUtils.Visibility.Visibles;

namespace GUtils.Loading.Extensions
{
    public static class LoadingServiceExtensions
    {
        public static void AddVisibleToBeforeAndAfterLoading(this ILoadingService loadingService, IVisible visible)
        {
            loadingService.AddBeforeLoading((instantly, ct) => visible.SetVisible(true, instantly, ct));
            loadingService.AddAfterLoading((instantly, ct) => visible.SetVisible(false, instantly, ct));
        }
    }
}
