using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Threading;

namespace LemonadeStand
{


    public class AmazonUpload
    {


        //full disclosure: my little brother and I cludged this together from a few stackoverflow questions
        //for use on his website last sunday.
        //this is the dirty hack version, the access keys should be in the app.config 
        //and app.config should be added to gitignore.
        public bool sendMyFileToS3(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            string accessKey = "AKIAWXS64BGGY47QHRWQ";
            string secretKey = "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";
            AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast2);
            // input explained :
            // localFilePath = the full local file path e.g. "c:\mydir\mysubdir\myfilename.zip"
            // bucketName : the name of the bucket in S3 ,the bucket should be alreadt created
            // subDirectoryInBucket : if this string is not empty the file will be uploaded to
            // a subdirectory with this name
            // fileNameInS3 = the file name in the S3
            // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
            // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
            // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
            // store your file in a different cloud storage but (i think) it differ in performance
            // depending on your location
            //IAmazonS3 client = new AmazonS3Client(RegionEndpoint.USEast2);
            // create a TransferUtility instance passing it the IAmazonS3 created in the first step


            TransferUtility utility = new TransferUtility(client);


            // making a TransferUtilityUploadRequest instance


            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3; //file name up in S3
            request.FilePath = localFilePath; //local file name
            utility.Upload(request); //commensing the transfer
            return true; //indicate that the file was sent
        }
        public Stream getMyFilesFromS3(string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            string accessKey = "AKIAWXS64BGGY47QHRWQ";
            string secretKey = "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";
            AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast2);
            // input explained :
            // localFilePath = the full local file path e.g. "c:\mydir\mysubdir\myfilename.zip"
            // bucketName : the name of the bucket in S3 ,the bucket should be alreadt created
            // subDirectoryInBucket : if this string is not empty the file will be uploaded to
            // a subdirectory with this name
            // fileNameInS3 = the file name in the S3
            // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
            // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
            // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
            // store your file in a different cloud storage but (i think) it differ in performance
            // depending on your location
            //IAmazonS3 client = new AmazonS3Client(RegionEndpoint.USEast2);
            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityOpenStreamRequest request = new TransferUtilityOpenStreamRequest();
            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3; //file name up in S3
            Stream stream = utility.OpenStream(request); //commensing the transfer
            return stream; //indicate that the file was sent
        }
    }
}
    

