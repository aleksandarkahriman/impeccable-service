using System;
using Amazon.S3;
using Amazon.S3.Model;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.FileStorage
{
    internal class FileStorage : IFileStorage
    {
        private const string BucketName = "aws-test-is-eu-west-1-773924021256-test-is-file-storage";

        private readonly IAmazonS3 _s3Client;

        public FileStorage(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public string Sign<T>(T file) where T : File
        {
            try
            {
                var signedUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = BucketName,
                    Key = file.Path,
                    Expires = DateTime.Now.AddMinutes(1),
                    Verb = HttpVerb.GET
                };
                var signedUrl = _s3Client.GetPreSignedURL(signedUrlRequest);
                return signedUrl;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
                throw;
            }
        }
    }
}
