using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RaceDay.Api.Services
{
    public class BlobStorageService
    {
        // Stores the application configuration so we can access Azure settings.
        private readonly IConfiguration _configuration;

        // Constructor receives configuration through dependency injection.
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Uploads an image to Azure Blob Storage and returns its URL.
        public async Task<string> UploadImageAsync(
            Stream fileStream,
            string fileName,
            string contentType)
        {
            // Retrieve the Azure connection string from appsettings.json.
            string connectionString =
                _configuration["AzureBlobStorage:ConnectionString"]
                ?? throw new InvalidOperationException(
                    "Azure Storage connection string is missing.");

            // Retrieve the name of the Azure storage container.
            string containerName =
                _configuration["AzureBlobStorage:ContainerName"]
                ?? throw new InvalidOperationException(
                    "Azure container name is missing.");

            // Connect to the Azure Blob Storage account.
            BlobServiceClient blobServiceClient =
                new BlobServiceClient(connectionString);

            // Access the container where uploaded images will be stored.
            BlobContainerClient containerClient =
                blobServiceClient.GetBlobContainerClient(containerName);

            // Create the container if it does not already exist.
            await containerClient.CreateIfNotExistsAsync(
                PublicAccessType.None);

            // Generate a unique filename to prevent files from overwriting each other.
            string uniqueFileName =
                $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";

            // Create a reference to the image that will be uploaded.
            BlobClient blobClient =
                containerClient.GetBlobClient(uniqueFileName);

            // Upload the image and specify its content type.
            await blobClient.UploadAsync(
                fileStream,
                new BlobHttpHeaders { ContentType = contentType });

            // Return the uploaded image's URL.
            return blobClient.Uri.ToString();
        }
    }
}
