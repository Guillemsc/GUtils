using System;
using GUtils.ApplicationContexts.Handlers;

namespace GUtils.ApplicationContexts.State;

public sealed class ApplicationContextState
{
    public ApplicationContextState(
        Type type, 
        IApplicationContextHandler handler
        )
    {
        Type = type;
        Handler = handler;
    }

    public Type Type { get; }
    public IApplicationContextHandler Handler { get; }
    
    public bool Loaded { get; set; }
}