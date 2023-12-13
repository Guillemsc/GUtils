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
            
            IApplicationContextHandler handler = applicationContextService.Push(applicationContext);

            loadingContext
                .Enqueue(ct => handler.Load())
                .EnqueueAfterLoad(handler.Start);

            return loadingContext;
        }
        
        public static ILoadingContext EnqueueUnloadApplicationContext(
            this ILoadingContext loadingContext,
            IApplicationContext applicationContext
        )
        {
            IApplicationContextService applicationContextService = ServiceLocator.Get<IApplicationContextService>();
            
            IApplicationContextHandler handler = applicationContextService.Push(applicationContext);

            loadingContext.Enqueue(ct => handler.Unload());

            return loadingContext;
        }
    }
}
