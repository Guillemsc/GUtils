using System.Threading;
using GUtils.Persistence.Data;
using GUtils.Extensions;

namespace GUtils.Persistence.Extensions
{
    public static class PersistenceDataExtensions
    {
        public static void SaveAsync(this IPersistenceData persistenceData)
        {
            persistenceData.Save(CancellationToken.None).FireAndForget();
        }
    }
}
