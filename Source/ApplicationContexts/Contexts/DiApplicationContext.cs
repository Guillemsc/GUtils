using System.Threading;
using System.Threading.Tasks;
using GUtils.Di.Contexts;
using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace GUtils.ApplicationContexts.Contexts
{
    public abstract class DiApplicationContext<IInteractor> : IApplicationContext
        where IInteractor : ILoadable, IStartable, ITaskDisposable
    {
        IDiContext<IInteractor>? _diContext;
        IDisposable<IInteractor>? _interactor;

        public Task Load(CancellationToken cancellationToken)
        {
            _diContext = new DiContext<IInteractor>();

            Install(_diContext);

            _interactor = _diContext.Install();

            return _interactor.Value.Load(cancellationToken);
        }

        public void Start()
        {
            _interactor!.Value.Start();
        }

        public async Task Dispose(CancellationToken cancellationToken)
        {
            await _interactor!.Value.Dispose(cancellationToken);

            _interactor!.Dispose();
        }

        protected abstract void Install(IDiContext<IInteractor> context);
    }
}