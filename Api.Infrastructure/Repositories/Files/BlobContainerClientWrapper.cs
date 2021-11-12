using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Api.Infrastructure.Repositories.Files
{
    public class BlobContainerClientWrapper : BlobContainerClient
    {
        private readonly BlobServiceClient _serviceClient;

        public BlobContainerClientWrapper(BlobServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<BlobContainerClient> GetOrCreate(string containerName, PublicAccessType accessType)
        {
            BlobContainerClient blobClient;
            try
            {
                blobClient = await _serviceClient.CreateBlobContainerAsync(containerName, accessType);
            }
            catch (Exception)
            {
                _serviceClient.GetBlobContainerClient(containerName);
                throw;
            }

            return blobClient;
        }
    }
}