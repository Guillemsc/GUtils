using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Delegates.Animation;
using GUtils.Extensions;
using GUtils.Loading.Loadables;
using GUtils.Tasks.Sequencing.Instructions;
using GUtils.Tasks.Sequencing.Sequencer;

namespace GUtils.Loading.Contexts
{
    /// <inheritdoc />
    public sealed class LoadingContext : ILoadingContext
    {
        readonly ITaskSequencer _taskSequencer;

        readonly IReadOnlyList<TaskAnimationEvent> _beforeLoad;
        readonly IReadOnlyList<TaskAnimationEvent> _afterLoad;

        readonly Queue<IInstruction> _enqueuedInstructions = new();
        readonly Queue<IInstruction> _afterLoadEnqueuedInstructions = new();

        bool _runBeforeLoadActionsInstantly;
        bool _dontRunAfterLoadActions;

        public bool IsLoading { get; private set; }

        public LoadingContext(
            ITaskSequencer taskSequencer,
            IReadOnlyList<TaskAnimationEvent> beforeLoad,
            IReadOnlyList<TaskAnimationEvent> afterLoad
            )
        {
            _taskSequencer = taskSequencer;
            _beforeLoad = beforeLoad;
            _afterLoad = afterLoad;
        }

        public ILoadingContext Enqueue(Func<CancellationToken, Task> function)
        {
            _enqueuedInstructions.Enqueue(new TaskInstruction(function));
            return this;
        }

        public ILoadingContext Enqueue(ILoadableAsync loadableAsync)
        {
            _enqueuedInstructions.Enqueue(new TaskInstruction(loadableAsync.LoadAsync));
            return this;
        }

        public ILoadingContext Enqueue(Action action)
        {
            _enqueuedInstructions.Enqueue(new ActionInstruction(action));
            return this;
        }

        public ILoadingContext EnqueueAfterLoad(params Action[] actions)
        {
            foreach (Action action in actions)
            {
                _afterLoadEnqueuedInstructions.Enqueue(new ActionInstruction(action));
            }

            return this;
        }

        public ILoadingContext RunBeforeLoadActionsInstantly()
        {
            _runBeforeLoadActionsInstantly = true;
            return this;
        }

        public ILoadingContext DontRunAfterLoadActions()
        {
            _dontRunAfterLoadActions = true;
            return this;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            await _taskSequencer.AwaitCompletition(cancellationToken);
            
            if(cancellationToken.IsCancellationRequested) return;

            IsLoading = true;

            foreach (TaskAnimationEvent before in _beforeLoad)
            {
                _taskSequencer.Play(ct => before.Invoke(_runBeforeLoadActionsInstantly, ct));
            }

            while (_enqueuedInstructions.Count > 0)
            {
                _taskSequencer.Play(_enqueuedInstructions.Dequeue());
            }

            if (!_dontRunAfterLoadActions)
            {
                foreach (TaskAnimationEvent after in _afterLoad)
                {
                    _taskSequencer.Play(ct => after.Invoke(false, ct));
                }
            }

            while (_afterLoadEnqueuedInstructions.Count > 0)
            {
                _taskSequencer.Play(_afterLoadEnqueuedInstructions.Dequeue());
            }

            await _taskSequencer.AwaitCompletition(cancellationToken);

            IsLoading = false;
        }

        public void ExecuteAsync()
        {
            Execute(CancellationToken.None).RunAsync();
        }
    }
}
