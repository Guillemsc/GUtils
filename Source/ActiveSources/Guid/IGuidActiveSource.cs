using System;

namespace GUtils.ActiveSources
{
    [Obsolete("Use IdBlockedInput<T> instead")]
    public interface IGuidActiveSource : IActiveSource
    {
        event Action<Guid, bool> OnActiveChanged;

        bool IsActive(Guid uid);
        void SetActive(object owner, Guid uid, bool active);
    }
}
