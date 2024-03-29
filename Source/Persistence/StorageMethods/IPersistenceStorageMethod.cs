using System.Threading;
using System.Threading.Tasks;
using GUtils.DiscriminatedUnions;
using GUtils.Optionals;
using GUtils.Types;

namespace GUtils.Persistence.StorageMethods
{
    /// <summary>
    /// Represents a storage method for saving and loading data.
    /// </summary>
    public interface IPersistenceStorageMethod
    {
        /// <summary>
        /// Saves the specified data to the specified local path.
        /// </summary>
        /// <param name="localPath">The local path where the data will be saved.</param>
        /// <param name="data">The data to be saved.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the saving operation (optional).</param>
        /// <returns>A task representing the saving operation. The task result contains an optional error
        /// message if an error occurs.</returns>
        Task<Optional<ErrorMessage>> Save(string localPath, string data, CancellationToken cancellationToken);

        /// <summary>
        /// Loads the data from the specified local path.
        /// </summary>
        /// <param name="localPath">The local path from where to load the data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the loading operation (optional).</param>
        /// <returns>A task representing the loading operation. The task result contains either
        /// the loaded data or an error message.</returns>
        Task<OneOf<string, ErrorMessage>> Load(string localPath, CancellationToken cancellationToken);
    }
}
