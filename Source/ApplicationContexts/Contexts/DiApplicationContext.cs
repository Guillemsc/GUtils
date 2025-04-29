using System.Threading;
using System.Threading.Tasks;
using GUtils.Di.Contexts;
using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace GUtils.ApplicationContexts.Contexts
{
    public abstract class DiApplicationContext<IInteractor> : IApplicationContext
        where IInteractor : ILoadableAsync, IStartable, ITaskDisposable
    {
        IDiContext<IInteractor>? _diContext;
        IDisposable<IInteractor>? _interactor;

        public Task LoadAsync(CancellationToken cancellationToken)
        {
            _diContext = new DiContext<IInteractor>();

            Install(_diContext);

            _interactor = _diContext.Install();

            return _interactor.Value.LoadAsync(cancellationToken);
        }

        public void Start()
        {
            _interactor!.Value.Start();
        }

        public async Task DisposeAsync()
        {
            await _interactor!.Value.DisposeAsync();

            _interactor!.Dispose();
        }

        protected abstract void Install(IDiContext<IInteractor> context);
    }
}