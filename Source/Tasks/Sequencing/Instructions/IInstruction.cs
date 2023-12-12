using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Tasks.Sequencing.Instructions
{
    public interface IInstruction
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
