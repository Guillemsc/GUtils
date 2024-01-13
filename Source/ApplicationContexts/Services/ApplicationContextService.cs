using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;
using GUtils.ApplicationContexts.State;
using GUtils.Optionals;
using GUtils.Tasks.Sequencing.Sequencer;

namespace GUtils.ApplicationContexts.Services
{
    public sealed class ApplicationContextService : IApplicationContextService
    {
        readonly ITaskSequencer _taskSequencer = new TaskSequencer();
        readonly List<ApplicationContextState> _states = new();

        public bool IsAnyPushed<T>() where T : IApplicationContextHandler
        {
            Type type = typeof(T);

            return IsAnyPushed(type);
        }
        
        public bool IsAnyPushed(Type type) 
        {
            foreach (ApplicationContextState state in _states)
            {
                if (state.ApplicationContext.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool IsAnyLoaded(Type type) 
        {
            foreach (ApplicationContextState state in _states)
            {
                if (state.ApplicationContext.GetType() == type)
                {
                    if (state.Loaded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public IApplicationContextHandler Push(IApplicationContext applicationContext)
        {
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

            ApplicationContextState state = new(applicationContext, handler);
            _states.Add(state);

            return handler;
        }

        public IApplicationContextHandler GetPushedUnsafe<T>() where T : IApplicationContext
        {
            Type type = typeof(T);

            foreach (ApplicationContextState state in _states)
            {
                bool isType = state.ApplicationContext.GetType() == type;

                if (isType)
                {
                    return state.Handler;
                }
            }

            throw new Exception();
        }

        Optional<ApplicationContextState> GetStateForContext(IApplicationContext applicationContext)
        {
            foreach (ApplicationContextState state in _states)
            {
                if (state.ApplicationContext == applicationContext)
                {
                    return state;
                }
            }

            return Optional<ApplicationContextState>.None;
        }

        Task LoadApplicationContext(IApplicationContext applicationContext)
        {
            Task Run(CancellationToken cancellationToken)
            {
                Optional<ApplicationContextState> optionalState = GetStateForContext(applicationContext);

                bool hasState = optionalState.TryGet(out ApplicationContextState state);

                if (!hasState)
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
            Optional<ApplicationContextState> optionalState = GetStateForContext(applicationContext);

            bool hasState = optionalState.TryGet(out ApplicationContextState state);

            if (!hasState)
            {
                return;
            }

            if (!state!.Loaded)
            {
                throw new Exception("Tried to start ApplicationContext, but it was not loaded");
            }

            applicationContext.Start();
        }

        Task UnloadApplicationContext(IApplicationContext applicationContext)
        {
            Task Run(CancellationToken cancellationToken)
            {
                Optional<ApplicationContextState> optionalState = GetStateForContext(applicationContext);

                bool hasState = optionalState.TryGet(out ApplicationContextState state);

                if (!hasState)
                {
                    return Task.CompletedTask;
                }

                _states.Remove(state);

                if (!state!.Loaded)
                {
                    throw new Exception("Tried to unload ApplicationContext, but it was not loaded");
                }

                state!.Loaded = false;

                return applicationContext.Dispose(cancellationToken);
            }

            return _taskSequencer.PlayAndAwait(Run);
        }
    }
}