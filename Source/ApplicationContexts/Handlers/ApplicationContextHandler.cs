using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.ApplicationContexts.Handlers
{
    public sealed class ApplicationContextHandler : IApplicationContextHandler
    {
        readonly Func<Task> _loadAction;
        readonly Action _startAction;
        readonly Func<Task> _unloadAction;

        public ApplicationContextHandler(
            Func<Task> loadAction,
            Action startAction,
            Func<Task> unloadAction
        )
        {
            _loadAction = loadAction;
            _startAction = startAction;
            _unloadAction = unloadAction;
        }

        public Task Load()
            => _loadAction.Invoke();

        public void Start()
            => _startAction.Invoke();

        public Task Unload()
            => _unloadAction.Invoke();
    }
}