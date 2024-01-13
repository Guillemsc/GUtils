using System;
using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;
using GUtils.ApplicationContexts.Services;
using GUtils.Loading.Contexts;
using GUtils.Services.Locators;

namespace GUtils.Loading.Extensions
{
    public static class LoadingContextExtensions
    {
        public static ILoadingContext EnqueueGCCollect(this ILoadingContext loadingContext)
        {
            return loadingContext.Enqueue(GC.Collect);
        }
        
        public static ILoadingContext EnqueueLoadAndStartApplicationContext(
            this ILoadingContext loadingContext,
            IApplicationContext applicationContext
        )
        {
            IApplicationContextService applicationContextService = ServiceLocator.Get<IApplicationContextService>();

            IApplicationContextHandler? handler = null;

            loadingContext
                .Enqueue(ct =>
                {
                    bool someLoaded = applicationContextService.IsAnyLoaded(applicationContext.GetType());

                    if (someLoaded)
                    {
                        throw new Exception("Tried to load multiple ApplicationContexts of the same type");
                    }
                    
                    handler = applicationContextService.Push(applicationContext);
                    
                    return handler.Load();
                })
                .EnqueueAfterLoad(() => handler!.Start());

            return loadingContext;
        }
        
        public static ILoadingContext EnqueueUnloadApplicationContext<T>(
            this ILoadingContext loadingContext
        ) where T : IApplicationContext
        {
            IApplicationContextService applicationContextService = ServiceLocator.Get<IApplicationContextService>();
            
            loadingContext.Enqueue(ct =>
            {
                IApplicationContextHandler handler = applicationContextService.GetPushedUnsafe<T>();
                return handler.Unload();
            });

            return loadingContext;
        }
    }
}
