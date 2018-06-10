using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic.Helpers
{
    
    public static class AmazonS3Helper
    {
        static string accessKey = "AKIAJGBB4EM4E4J6WBVQ";
        static string secretKey = "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";
        static string bucketName = "alrajhisolutions";
        static string folderName = "PMSystem";
        static string folderNameLog = "logs";
        static IAmazonS3 client;


        public static bool WritingAnObject(Stream file,string contentType, string fileName)
        {
            
            try
            {

                client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, RegionEndpoint.EUCentral1);
                var transferUtility = new TransferUtility(client);
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    Key = folderName+"/"+ fileName,
                    InputStream = file,
                    ContentType = contentType,
                    StorageClass = S3StorageClass.ReducedRedundancy,
                    CannedACL = S3CannedACL.PublicRead
                };
                transferUtility.Upload(request);

                return true;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    //Console.WriteLine("Check the provided AWS Credentials.");
                    //Console.WriteLine(
                    //"For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    //Console.WriteLine(
                    //    "Error occurred. Message:'{0}' when writing an object"
                    //    , amazonS3Exception.Message);
                }
                return false;
            }
        }
      


       
    }
}
