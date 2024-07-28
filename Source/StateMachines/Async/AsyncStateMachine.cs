#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace GUtils.StateMachines.Async
{
    public sealed class AsyncStateMachine
    {
        IAsyncState? _currentState;
        bool _changingState;
        
        public async Task Set(IAsyncState asyncState)
        {
            if (_changingState)
            {
                return;
            }

            _changingState = true;
            
            if (_currentState != null)
            {
                await _currentState.Dispose(CancellationToken.None);
            }

            _currentState = asyncState;

            await _currentState.Load(CancellationToken.None);
            
            _currentState.Start();

            _changingState = false;
        }
    }
}