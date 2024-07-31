#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.StackedStateMachines.Async
{
    public sealed class StackedAsyncStateMachine
    {
        readonly List<IAsyncState> _stack = new();
        bool _changingState;
        
        public async Task Push(IAsyncState asyncState)
        {
            if (_changingState)
            {
                return;
            }

            _changingState = true;

            await asyncState.LoadAsync(CancellationToken.None);
            
            asyncState.Start();
            
            _stack.Add(asyncState);

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

            await state.DisposeAsync(CancellationToken.None);
            
            _stack.RemoveAt(lastIndex);

            _changingState = false;
        }
    }
}