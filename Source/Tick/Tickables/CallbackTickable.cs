using System;

namespace GUtils.Tick.Tickables
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents a tickable object that invokes a callback action during each tick.
    /// </summary>
    public sealed class CallbackTickable : ITickable
    {
        readonly Action _tick;

        public CallbackTickable(Action tick)
        {
            _tick = tick;
        }

        public void Tick()
        {
            _tick.Invoke();
        }
    }
}
