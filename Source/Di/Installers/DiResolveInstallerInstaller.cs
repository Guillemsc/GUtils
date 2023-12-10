using GUtils.Di.Builder;
using GUtils.Di.Container;

namespace GUtils.Di.Installers
{
    public sealed class DiResolveInstallerInstaller<T> : IInstaller
    {
        readonly IDiResolveContainer _diResolveContainer;

        public DiResolveInstallerInstaller(IDiResolveContainer diResolveContainer)
        {
            _diResolveContainer = diResolveContainer;
        }

        public void Install(IDiContainerBuilder builder)
        {
            builder.Bind<T>().FromFunction(() => _diResolveContainer.Resolve<T>());
        }
    }
}
