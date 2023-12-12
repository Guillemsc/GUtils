using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;
using GUtils.ApplicationContexts.State;
using GUtils.Tasks.Sequencing.Sequencer;

namespace GUtils.ApplicationContexts.Services;

public sealed class ApplicationContextService : IApplicationContextService
{
    readonly ITaskSequencer _taskSequencer = new TaskSequencer();
    readonly Dictionary<Type, ApplicationContextState> _states = new();
    
    public IApplicationContextHandler Push(IApplicationContext applicationContext)
    {
        Type type = applicationContext.GetType();

        bool alreadyPushed = _states.TryGetValue(type, out ApplicationContextState? state);

        if (alreadyPushed)
        {
            return state!.Handler;
        }
        
        Task Load()
            => LoadApplicationContext(applicationContext);
        
        void Start()
            => StartApplicationContext(applicationContext);
        
        Task Unload()
            => UnloadApplicationContext(applicationContext);

        ApplicationContextHandler handler = new ApplicationContextHandler(
            Load,
            Start,
            Unload
        );

        state = new ApplicationContextState(type, handler);
        _states.Add(type, state);

        return handler;
    }

    Task LoadApplicationContext(IApplicationContext applicationContext)
    {
        Task Run(CancellationToken cancellationToken)
        {
            Type type = applicationContext.GetType();

            bool alreadyPushed = _states.TryGetValue(type, out ApplicationContextState? state);

            if (!alreadyPushed)
            {
                return Task.CompletedTask;
            }

            if (state!.Loaded)
            {
                return Task.CompletedTask;
            }
            
            state!.Loaded = true;

            return applicationContext.Load(cancellationToken);
        }

        return _taskSequencer.PlayAndAwait(Run);
    }

    void StartApplicationContext(IApplicationContext applicationContext)
    {
        Type type = applicationContext.GetType();

        bool alreadyPushed = _states.TryGetValue(type, out ApplicationContextState? state);

        if (!alreadyPushed)
        {
            return;
        }

        if (!state!.Loaded)
        {
            return;
        }
        
        applicationContext.Start();
    }

    Task UnloadApplicationContext(IApplicationContext applicationContext)
    {
        Task Run(CancellationToken cancellationToken)
        {
            Type type = applicationContext.GetType();

            bool alreadyPushed = _states.TryGetValue(type, out ApplicationContextState? state);

            if (!alreadyPushed)
            {
                return Task.CompletedTask;
            }

            if (!state!.Loaded)
            {
                return Task.CompletedTask;
            }
            
            state!.Loaded = false;

            return applicationContext.Dispose(cancellationToken);
        }

        return _taskSequencer.PlayAndAwait(Run);
    }
}