using System;
using GUtils.Di.Builder;

namespace GUtils.Di.Installers
{
    public sealed class CallbackInstaller : IInstaller
    {
        readonly Action<IDiContainerBuilder> _callback;

        public CallbackInstaller(
            Action<IDiContainerBuilder> callback
            )
        {
            _callback = callback;
        }

        public void Install(IDiContainerBuilder container)
        {
            _callback.Invoke(container);
        }
    }
}
