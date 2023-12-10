using System;

namespace GUtils.ActiveSources
{
    public sealed class NopIndexActiveSource : IIndexActiveSource
    {
        public static readonly NopIndexActiveSource Instance = new();
        
        public event Action<int, bool>? OnActiveStateChanged;

        NopIndexActiveSource()
        {
        }

        public bool IsActive(int index)
        {
            return true;
        }

        public void SetActive(object owner, int index, bool active)
        {
        }

        public void SetBlocked(int index, bool blocked)
        {
        }

        public void DeactivateAll(object owner)
        {
        }

        public void ActivateAll(object owner)
        {
        }

        public void SetActiveAll(
            object owner,
            bool active)
        {
        }
    }
}
