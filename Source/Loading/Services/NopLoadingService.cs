using GUtils.Delegates.Animation;
using GUtils.Loading.Contexts;

namespace GUtils.Loading.Services
{
    public sealed class NopLoadingService : ILoadingService
    {
        public static readonly NopLoadingService Instance = new();

        public bool IsLoading => false;

        NopLoadingService()
        {

        }

        public void AddBeforeLoading(TaskAnimationEvent func) { }
        public void AddAfterLoading(TaskAnimationEvent func) { }
        public ILoadingContext New() => NopLoadingContext.Instance;
    }
}
