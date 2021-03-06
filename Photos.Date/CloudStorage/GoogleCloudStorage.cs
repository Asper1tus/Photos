using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Photos.Date.CloudStorage
{
    public class GoogleCloudStorage : ICloudStorage
    {
        readonly GoogleCredential googleCredential;
        readonly StorageClient storageClient;
        readonly string bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task<string> UploadFileAsync(Stream imageStream, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, imageStream);
                return dataObject.MediaLink;
            }
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }

    }
}
