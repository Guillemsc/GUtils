using GUtils.Caching.Cache;
using GUtils.Services.Locators;

namespace GUtils.Services.Cache
{
    public sealed class ServiceLocatorCached<T> : CachedResult<T>
    {
        public ServiceLocatorCached() : base(ServiceLocator.Get<T>)
        {
        }
    }
}