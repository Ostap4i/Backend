using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.Reactive.Linq;

namespace DENMAP_SERVER.Service
{
    public class MediaService
    {
        private const string Endpoint = "34.116.253.154:9000";
        private const string AccessKey = "mediaUser";
        private const string SecretKey = "d7a5haXvyMpQi63";
        private const string BucketName = "mybucket";

        private static readonly IMinioClient _minioClient = new MinioClient()
                .WithEndpoint(Endpoint)
                .WithCredentials(AccessKey, SecretKey)
                .Build();

        public async Task<Stream> GetObjectAsync(string objectName)
        {
            try
            {
                StatObjectArgs statObjectArgs = new StatObjectArgs()
                                                    .WithBucket(BucketName)
                                                    .WithObject(objectName);
                await _minioClient.StatObjectAsync(statObjectArgs);

                MemoryStream memoryStream = new MemoryStream();

                GetObjectArgs getObjectArgs = new GetObjectArgs()
                                                  .WithBucket(BucketName)
                                                  .WithObject(objectName)
                                                  .WithCallbackStream((stream) =>
                                                  {
                                                      stream.CopyTo(memoryStream);
                                                  });
                await _minioClient.GetObjectAsync(getObjectArgs);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return memoryStream;
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
                return null;
            }
        }

        public async Task<string> GetObjectUrlAsync(string objectName)
        {
            try
            {
                StatObjectArgs statObjectArgs = new StatObjectArgs()
                                                    .WithBucket(BucketName)
                                                    .WithObject(objectName);
                await _minioClient.StatObjectAsync(statObjectArgs);

                PresignedGetObjectArgs presignedArgs = new PresignedGetObjectArgs()
                                                           .WithBucket(BucketName)
                                                           .WithObject(objectName)
                                                           .WithExpiry(24 * 60 * 60);
                
                string presignedUrl = await _minioClient.PresignedGetObjectAsync(presignedArgs);

                Console.WriteLine($"Generated presigned URL: {presignedUrl}");

                return presignedUrl;
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
                return null;
            }
        }

        public async Task<bool> PutObjectAsync(string bucketName, string objectName, string filePath, string contentType)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found at path: {filePath}");
                    return false;
                }

                Console.WriteLine($"Starting upload for file: {filePath} with objectName: {objectName}");

                PutObjectArgs putObjectArgs = new PutObjectArgs()
                                                  .WithBucket(bucketName)
                                                  .WithObject(objectName)
                                                  .WithFileName(filePath)
                                                  .WithContentType(contentType);

                await _minioClient.PutObjectAsync(putObjectArgs);

                Console.WriteLine($"File {filePath} uploaded successfully as {objectName}");
                return true;
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred during upload: " + e);
                return false;
            }
        }
    }
}
