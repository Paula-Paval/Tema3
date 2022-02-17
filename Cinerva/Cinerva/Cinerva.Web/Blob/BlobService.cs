using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace Cinerva.Web.Blob
{
    public class BlobService:IBlobService
    {
        public BlobServiceClient blobServiceClient { get; set; }
        public BlobContainerClient containerClient { get; set; }

        private string storageConnstring; 
        private string containerName; 
    

        public BlobService(IConfiguration configuration)
        {
            
             this.storageConnstring = configuration["Storage:StorageConnString"];
             this.containerName = configuration["Storage:ContainerName"];

             blobServiceClient = new BlobServiceClient(storageConnstring);
             containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        
        }

        public async Task<string> Upload(IFormFile image)
        {
          
            var  blobClient = containerClient.GetBlobClient(image.FileName);

            await blobClient.UploadAsync(image.OpenReadStream(), true);

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task Delete(string url)
        {
            var fileName= Path.GetFileName(url);

            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blob = blobContainerClient.GetBlobClient(fileName);

            await blob.DeleteIfExistsAsync();
        }

    }
}
