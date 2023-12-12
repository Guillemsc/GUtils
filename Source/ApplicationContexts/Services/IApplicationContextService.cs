using GUtils.ApplicationContexts.Contexts;
using GUtils.ApplicationContexts.Handlers;

namespace GUtils.ApplicationContexts.Services;

public interface IApplicationContextService
{
    IApplicationContextHandler Push(IApplicationContext applicationContext);
}