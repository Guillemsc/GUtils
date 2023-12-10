using GUtils.Di.Builder;
using GUtils.Disposing.Disposables;

namespace GUtils.Di.Installers
{
    public interface IInstaller
    {
        void Install(IDiContainerBuilder builder);
    }
}
