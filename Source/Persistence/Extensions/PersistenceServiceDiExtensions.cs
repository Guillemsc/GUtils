using GUtils.Di.Builder;
using GUtils.Di.Container;
using GUtils.Persistence.Data;
using GUtils.Persistence.Services;

namespace GUtils.Persistence.Extensions
{
    public static class PersistenceServiceDiExtensions
    {
        public static IDiBindingActionBuilder<T> FromPersistenceService<T>(
            this IDiBindingBuilder<T> diBindingBuilder
            ) where T : IPersistenceData
        {
            T FromFunction(IDiResolveContainer resolveContainer)
            {
                IPersistenceService persistenceService = resolveContainer.Resolve<IPersistenceService>();
                return persistenceService.Get<T>();
            }

            return diBindingBuilder.FromFunction(FromFunction);
        }
    }
}
