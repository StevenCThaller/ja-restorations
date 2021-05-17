using System;
using System.Collections.Generic;
using backend.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
// using Amazon.S3.Transfer;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class ImageService
    {
        
        public async static void DeleteImages(IEnumerable<S3Image> images)
        {

            DeleteObjectsRequest keysToDelete = new DeleteObjectsRequest(){ BucketName = AppSettings.appSettings.BucketName };
            foreach(S3Image image in images)
            {   
                string[] split = image.url.Split('/');
                keysToDelete.AddKey(split[split.Length-2]+'/'+split[split.Length-1]);
                
            }
            IAmazonS3 client = new AmazonS3Client(AppSettings.appSettings.AWSAccessKey, AppSettings.appSettings.AWSSecretKey, RegionEndpoint.USEast2);

            await client.DeleteObjectsAsync(keysToDelete);
        }
    }
}