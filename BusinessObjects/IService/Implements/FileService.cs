using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessObjects.RequestModels;
using System;
using System.IO;
using System.Linq;

namespace BusinessObjects.IService.Implements
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "bfrsimage";

        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> Upload(FileRequest fileRequest)
        {

            var containerInstance = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobName = Path.GetFileName(fileRequest.imageFile.FileName);

            var blobInstance = containerInstance.GetBlobClient(blobName);
            await blobInstance.UploadAsync(fileRequest.imageFile.OpenReadStream());

            return blobInstance.Uri.ToString();
        }

        public async Task<Stream> Get(string name)
        {
            var containerInstance = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobInstance = containerInstance.GetBlobClient(name);
            var downloadContent = await blobInstance.DownloadAsync();
            return downloadContent.Value.Content;
        }

        private bool IsImageFile(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            string extension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}
