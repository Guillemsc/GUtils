using System;
using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;
using GUtils.ApplicationContexts.Services;
using GUtils.Loading.Contexts;

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
            IApplicationContextService applicationContextService,
            IApplicationContext applicationContext
        )
        {
            IApplicationContextHandler handler = applicationContextService.Push(applicationContext);

            loadingContext
                .Enqueue(ct => handler.Load())
                .EnqueueAfterLoad(handler.Start);

            return loadingContext;
        }
        
        public static ILoadingContext EnqueueUnloadApplicationContext(
            this ILoadingContext loadingContext, 
            IApplicationContextService applicationContextService,
            IApplicationContext applicationContext
        )
        {
            IApplicationContextHandler handler = applicationContextService.Push(applicationContext);

            loadingContext.Enqueue(ct => handler.Unload());

            return loadingContext;
        }
    }
}
