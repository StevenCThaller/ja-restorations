using System;
using System.Collections.Generic;
using backend.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
// using Amazon.S3.Transfer;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IImageService
    {
        Task<IEnumerable<S3Image>> GetAllImages();
        Task<IEnumerable<S3Image>> GetXMostRecentFurnitureImages(int count);
        Task<IEnumerable<S3Image>> GetXMostRecentGalleryImages(int count);
    }
    public class ImageService : IImageService
    {
        private MyContext _context;

        public ImageService(MyContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<S3Image>> GetAllImages()
        {
            await Task.Delay(0);
            return _context.Images;
        }
        public async Task<IEnumerable<S3Image>> GetXMostRecentFurnitureImages(int count)
        {
            await Task.Delay(0);
            return _context.Furniture
                .Include(f => f.images)
                .ThenInclude(i => i.s3Image)
                .OrderByDescending(f => f.createdAt)
                .Where(f => f.images.Count > 0)
                .Take(count)
                .Select(f => f.images.First().s3Image);
        }

        public async Task<IEnumerable<S3Image>> GetXMostRecentGalleryImages(int count)
        {
            await Task.Delay(0);
            return _context.GalleryImages
                .Include(g => g.s3Image)
                .OrderByDescending(g => g.createdAt)
                .Take(count)
                .Select(g => g.s3Image);
        }
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