using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.StackedStateMachines.Async
{
    public sealed class StackedAsyncStateMachine
    {
        readonly List<IAsyncState> _stack = new();
        bool _changingState;
        
        public async Task Push(IAsyncState asyncState, bool autoStart = true)
        {
            if (_changingState)
            {
                return;
            }

            _changingState = true;
            
            await asyncState.LoadAsync(CancellationToken.None);

            if (autoStart)
            {
                asyncState.Start();
            }
            
            _stack.Add(asyncState);

            _changingState = false;
        }

        public async Task Pop<T>() where T : IAsyncState
        {
            IAsyncState? state = GetState<T>();

            if (state == null)
            {
                return;
            }
            
            if (_changingState)
            {
                return;
            }

            _changingState = true;
            
            RemoveState(state);
            
            await state.DisposeAsync();
            
            _changingState = false;
        }

        public async Task Pop()
        {
            if (_changingState)
            {
                return;
            }

            _changingState = true;

            if (_stack.Count == 0)
            {
                return;
            }

            int lastIndex = _stack.Count - 1;

            IAsyncState state = _stack[lastIndex];

            _stack.RemoveAt(lastIndex);
            
            await state.DisposeAsync();

            _changingState = false;
        }

        IAsyncState? GetState<T>() where T : IAsyncState
        {
            Type type = typeof(T);
            
            for (int i = _stack.Count - 1; i >= 0; i--)
            {
                IAsyncState state = _stack[i];

                if (type == state.GetType())
                {
                    return state;
                }
            }

            return null;
        }

        void RemoveState(IAsyncState remove)
        {
            for (int i = _stack.Count - 1; i >= 0; i--)
            {
                IAsyncState state = _stack[i];

                if (state == remove)
                {
                    _stack.RemoveAt(i);
                    return;
                }
            }
        }
    }
}