using System.Collections.Generic;
using GUtils.Tick.Services;
using GUtils.Tick.Tickables;

namespace GUtils.Tick.Tickables
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents a <see cref="ITickable"/> object that manages a collection of other tickable objects.
    /// </summary>
    public sealed class TickablesContainerTickable : ITickable
    {
        readonly List<ITickable> _tickablesToAdd = new();
        readonly List<ITickable> _tickablesToRemove = new();

        readonly List<ITickable> _tickables = new();

        public bool IsTicking { get; private set; }
        
        public void Tick()
        {
            ActuallyRemoveTickables();

            IsTicking = true;
            
            TickTickables();
            
            IsTicking = false;

            ActuallyAddTickables();
        }

        /// <summary>
        /// Adds a tickable object to the container.
        /// </summary>
        /// <param name="tickable">The tickable object to add.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the tickable parameter is null.</exception>
        /// <exception cref="System.Exception">Thrown when the tickable is already added to the container.</exception>
        public void Add(ITickable tickable)
        {
            if (tickable == null)
            {
                throw new System.ArgumentNullException(
                    $"Tried to add {nameof(ITickable)} but it was null at {nameof(TickablesService)}"
                    );
            }

            bool contains = _tickables.Contains(tickable);

            if (contains)
            {
                throw new System.Exception(
                    $"Tried to add {nameof(ITickable)} but it was already at {nameof(TickablesService)}"
                    );
            }

            bool alreadyToAdd = _tickablesToAdd.Contains(tickable);

            if(alreadyToAdd)
            {
                return;
            }

            _tickablesToAdd.Add(tickable);
        }

        /// <summary>
        /// Removes a tickable object from the container.
        /// </summary>
        /// <param name="tickable">The tickable object to remove.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the tickable parameter is null.</exception>

        public void Remove(ITickable tickable)
        {
            if (tickable == null)
            {
                throw new System.ArgumentNullException(
                    $"Tried to remove {nameof(ITickable)} but it was null at {nameof(TickablesService)}"
                    );
            }

            bool contained = _tickables.Contains(tickable);

            if (!contained)
            {
                return;
            }

            bool alreadyToRemove = _tickablesToRemove.Contains(tickable);

            if (alreadyToRemove)
            {
                return;
            }

            _tickablesToRemove.Add(tickable);
        }

        /// <summary>
        /// Clears the container by removing all tickable objects.
        /// </summary>
        public void Clear()
        {
            _tickablesToRemove.AddRange(_tickables);

            ActuallyRemoveTickables();
        }

        void ActuallyAddTickables()
        {
            foreach(ITickable tickable in _tickablesToAdd)
            {
                _tickables.Add(tickable);
            }

            _tickablesToAdd.Clear();
        }

        public void ActuallyRemoveTickables()
        {
            if (IsTicking)
            {
                return;
            }
            
            foreach (ITickable tickable in _tickablesToRemove)
            {
                _tickables.Remove(tickable);
            }

            _tickablesToRemove.Clear();
        }

        void TickTickables()
        {
            foreach (ITickable tickable in _tickables)
            {
                tickable.Tick();
            }
        }
    }
}
