using GUtils.Caching.Cache;
using GUtils.Services.Locators;

namespace GUtils.Services.Cache
{
    public sealed class ServiceLocatorCachedResult<T> : CachedResult<T>
    {
        public ServiceLocatorCachedResult() : base(ServiceLocator.Get<T>)
        {
        }
    }
}