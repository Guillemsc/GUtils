using System.Threading;
using System.Threading.Tasks;

namespace GUtils.ApplicationContexts.Handlers;

public interface IApplicationContextHandler
{
    Task Load();
    void Start();
    Task Unload();
}