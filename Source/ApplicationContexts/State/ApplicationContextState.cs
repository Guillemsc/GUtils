using System;
using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;

namespace GUtils.ApplicationContexts.State
{
    public sealed class ApplicationContextState
    {
        public IApplicationContext ApplicationContext { get; }
        public IApplicationContextHandler Handler { get; }

        public bool Loaded { get; set; }

        public ApplicationContextState(
            IApplicationContext applicationContext,
            IApplicationContextHandler handler
        )
        {
            ApplicationContext = applicationContext;
            Handler = handler;
        }
    }
}