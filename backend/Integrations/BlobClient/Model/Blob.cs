using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using POD.Common.Core.Enum;

namespace POD.Integrations.BlobClient.Model
{
    public class Blob
    {
        public string Name { get; set; }
        public BlobUploadOptions Options { get; set; }
        
        private ContentType _type; // Backing field for Type
        public ContentType Type
        {
            get => _type;
            
            set
            {
                _type = value;
                
                Options = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = value.ToString()
                    }
                };
            }
        }
        
        public string Container { get; set; }
        private Stream _stream;
        public Stream Stream 
        { 
            get => _stream;
            private set => _stream = value;
        }
        private byte[]? _bytes;
        public byte[]? Bytes
        {
            get => _bytes;
            set
            {
                _bytes = value;
                UpdateStreamFromBytes(value);
            }
        }

        private string? _text;
        public string? Text
        {
            get => _text;
            set
            {
                _text = value;
                UpdateStreamFromText(value);
            }
        }
        
        private void UpdateStreamFromBytes(byte[]? bytes)
        {
            if (bytes != null)
            {
                Stream = new MemoryStream(bytes);
            }
            else
            {
                Stream = null;
            }
        }

        private void UpdateStreamFromText(string? text)
        {
            if (text != null)
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                Stream = new MemoryStream(bytes);
            }
            else
            {
                Stream = null;
            }
        }
        
        public async Task<BlockBlobClient> GetBlockBlobClient(Options.BlobClientConfiguration configuration)
        {
            var cloudBlobContainer = await CreateBlobContainerIfNotExists(configuration);
            return cloudBlobContainer.GetBlockBlobClient(Name);
        }

        private async Task<BlobContainerClient> CreateBlobContainerIfNotExists(Options.BlobClientConfiguration configuration)
        {
            var blobContainerClient = new BlobContainerClient(configuration.ConnectionString, Container);
            var isContainerExists = await blobContainerClient.ExistsAsync();
            if (isContainerExists)
            {
                return blobContainerClient;
            }

            return await CreateBlobContainer(configuration);
        }
        
        private async Task<BlobContainerClient> CreateBlobContainer(Options.BlobClientConfiguration configuration)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(configuration.ConnectionString);
            // //COMMENT OUT FOR DEV STORAGE EMULATOR
            // await blobServiceClient.SetPropertiesAsync(new BlobServiceProperties()
            // {
            //     Cors = new List<BlobCorsRule>()
            //     {
            //         new BlobCorsRule()
            //         {
            //             AllowedHeaders = "*",
            //             AllowedMethods = "GET",
            //             AllowedOrigins = "*",
            //             MaxAgeInSeconds = 1 * 60 * 60,
            //         }
            //     }
            // });

            BlobContainerClient blobContainerClient = await blobServiceClient.CreateBlobContainerAsync(Container);
            // //COMMENT OUT FOR DEV STORAGE EMULATOR
            // await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            return blobContainerClient;
        }
    }
}
