using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Repositories
{
    public interface IImageRepository
    {
        /// <summary>
        /// Insert image to image repository.
        /// </summary>
        /// <param name="containerName">Name of the whole container</param>
        /// <param name="fileName">Name of the file, i.e. offer_[guid]</param>
        /// <param name="image">Image in byte</param>
        Task<string> InsertImage(string containerName, string fileName, byte[] image);

        Task<IEnumerable<string>> InsertImages(string containerName, Dictionary<string, byte[]> images);

        Task<string> FetchImageUrl(string containerName, string fileName);

        Task<IEnumerable<string>> FetchImagesUrls(string containerName, string fileName);
    }
}