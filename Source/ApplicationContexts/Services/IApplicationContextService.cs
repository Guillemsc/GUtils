using System;
using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;

namespace GUtils.ApplicationContexts.Services
{
    public interface IApplicationContextService
    {
        bool IsAnyPushed<T>() where T : IApplicationContextHandler;
        bool IsAnyPushed(Type type);
        bool IsAnyLoaded(Type type);
        IApplicationContextHandler Push(IApplicationContext applicationContext);
        IApplicationContextHandler GetPushedUnsafe<T>() where T : IApplicationContext;
    }
}