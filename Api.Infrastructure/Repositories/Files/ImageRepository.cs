using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Repositories;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Infrastructure.Repositories.Files
{
    public class ImageRepository : IImageRepository
    {
        private readonly ILogger<ImageRepository> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClientWrapper _blobContainerClient;


        public ImageRepository(IConfiguration configuration, ILogger<ImageRepository> logger)
        {
            _logger = logger;
            _blobServiceClient = new BlobServiceClient(configuration["blob:address"]);
            _blobContainerClient = new BlobContainerClientWrapper(_blobServiceClient);
        }

        public async Task<string> InsertImage(string containerName, string fileName, byte[] image)
        {
            var blobClient = await _blobContainerClient.GetOrCreate(containerName, PublicAccessType.BlobContainer);

            await using var stream = new MemoryStream();
            await stream.WriteAsync(image);
            stream.Position = 0;

            await blobClient.UploadBlobAsync(fileName, stream);

            return blobClient.GetBlobClient(fileName).Uri.AbsoluteUri;
        }

        public async Task<IEnumerable<string>> InsertImages(string containerName, Dictionary<string, byte[]> images)
        {
            var returnResult = new List<string>();

            if (!images.Any())
                return returnResult;

            var blobClient = await _blobContainerClient.GetOrCreate(containerName, PublicAccessType.BlobContainer);

            await using var stream = new MemoryStream();
            foreach (var (name, image) in images)
            {
                await stream.WriteAsync(image);
                stream.Position = 0;

                await blobClient.UploadBlobAsync(name, stream);

                var uri = blobClient.GetBlobClient(name).Uri.AbsoluteUri;

                returnResult.Add(uri);
            }

            return returnResult;
        }

        public async Task<string> FetchImageUrl(string containerName, string fileName)
        {
            var client = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = client.GetBlobClient(fileName);

            return blobClient.Uri.AbsoluteUri;
        }

        public Task<IEnumerable<string>> FetchImagesUrls(string containerName, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}