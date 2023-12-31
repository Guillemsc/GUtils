using GUtils.Tick.Enums;
using GUtils.Tick.Tickables;

namespace GUtils.Tick.Services
{
    public sealed class NopTickablesService : ITickablesService
    {
        public static readonly NopTickablesService Instance = new();

        NopTickablesService() { }

        public void Add(ITickable tickable, TickType tickType) { }
        public void Remove(ITickable tickable, TickType tickType) { }
        public void RemoveNow(ITickable tickable, TickType tickType) { }
        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}
