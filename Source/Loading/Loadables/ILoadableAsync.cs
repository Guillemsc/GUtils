using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Loading.Loadables
{
    /// <summary>
    /// Represents an interface for an object that can be loaded asynchronously.
    /// </summary>
    public interface ILoadableAsync
    {
        /// <summary>
        /// Loads the object asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task LoadAsync();
    }
}
