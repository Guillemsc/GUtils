using System;

namespace GUtils.ActiveSources
{
    public sealed class NopSingleActiveSource : ISingleActiveSource
    {
        public static readonly NopSingleActiveSource Instance = new();
        
        public event Action<bool>? OnActiveChanged;

        NopSingleActiveSource()
        {
        }

        public bool IsActive()
        {
            return true;
        }

        public void SetActive(object owner,  bool active)
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
