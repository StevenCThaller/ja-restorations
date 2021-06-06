using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Helpers;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller 
    {
        private IAuthService _authService;
        private IUserService _userService;

        // public ImagesController(IAuthService authService)
        // {
        //     _authService = authService;
        // }
        private IImageService _imageService;
        private MyContext _context;
        private IAmazonS3 client; 
        public ImagesController(MyContext context, IImageService imageService, IAuthService authService, IUserService userService)
        {
            _userService = userService;
            _authService = authService;
            _context = context;
            _imageService = imageService;
            client = new AmazonS3Client(AppSettings.appSettings.AWSAccessKey, AppSettings.appSettings.AWSSecretKey, RegionEndpoint.USEast2);
        }

        [HttpDelete("{imageId}")]
        public async Task<JsonResult> DeleteImage(int imageId)
        {
            S3Image existing = _context.Images.FirstOrDefault(i => i.s3ImageId == imageId);
            if(existing == null)
            {
                return Json(new { message = "error", error = "Image does not exist. "});
            }

            string[] sections = existing.url.Split('/');
            System.Console.WriteLine(sections[sections.Length-2]+'/'+sections[sections.Length-1]);
            await client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest(){ BucketName = AppSettings.appSettings.BucketName, Key = sections[sections.Length-2]+'/'+sections[sections.Length-1] });

            _context.Remove(existing);
            _context.SaveChanges();
            
            return Json(new { message = "success", data = existing });
        }

        [AllowAnonymous]
        [HttpGet("furniture/{count}")]
        public async Task<IActionResult> GetMultipleFurnitureImages(int count)
        {
            try
            {
                IEnumerable<S3Image> imageUrls = await _imageService.GetXMostRecentFurnitureImages(count);
                return Ok(Json(new { message = "success", results = imageUrls }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }



        [HttpPost("furniture/{furnitureId}")]
        public async Task<IActionResult> UploadFurnitureImages(int furnitureId, [FromForm] FileModel files)
        {
            try 
            {
                if(!await _authService.AuthorizeByHeadersAndRoleId(Request, 2))
                {
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }

                Furniture existing = _context.Furniture.FirstOrDefault(f => f.furnitureId == furnitureId);
                if(existing == null) {
                    return Json(new { message = "error", error = "That furniture does not exist." });
                }
                List<S3Image> images = new List<S3Image>();
                for(int i = 0; i < files.FormFiles.Count; i++)
                {
                    var file = files.FormFiles[i];
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/s3", files.FileNames[i]);

                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        TransferUtility utility = new TransferUtility(client);
                        TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                        request.BucketName = $"{AppSettings.appSettings.BucketName}/{AppSettings.appSettings.FurnitureDir}";
                        request.Key = files.FileNames[i];
                        request.InputStream = stream;
                        utility.Upload(request);
                        FileInfo toDelete = new FileInfo(path);
                        toDelete.Delete();
                    }
                    
                    S3Image img = new S3Image()
                    {
                        url = $"https://{AppSettings.appSettings.BucketName}.s3.{AppSettings.appSettings.Region}.amazonaws.com/{AppSettings.appSettings.FurnitureDir}/{files.FileNames[i]}"
                    };
                    images.Add(img);
                    _context.Add(img);
                }

                _context.SaveChanges();
                

                ConnectImagesToFurniture(furnitureId, images);
                return Ok(Json(new { message = "success", data = images }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", data = ex.Message }));
            }
            
        }

        [HttpPost("users/{userId}")]
        public async Task<IActionResult> UploadProfileImage(int userId, [FromForm] FileModel files)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndUserId(Request, userId))
                {
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }

                User user = await _userService.GetUser(userId);

                var file = files.FormFiles[0];
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/s3", files.FileNames[0]);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    TransferUtility utility = new TransferUtility(client);
                    TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                    request.BucketName = $"{AppSettings.appSettings.BucketName}/{AppSettings.appSettings.UserDir}";
                    request.Key = files.FileNames[0];
                    request.InputStream = stream;
                    utility.Upload(request);
                    FileInfo toDelete = new FileInfo(path);
                    toDelete.Delete();
                }

                user.profilePicture = $"https://{AppSettings.appSettings.BucketName}.s3.{AppSettings.appSettings.Region}.amazonaws.com/{AppSettings.appSettings.UserDir}/{files.FileNames[0]}";
                _context.SaveChanges();

                return Ok(Json(new { message = "success", results = user }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", data = ex.Message }));
            }
        }

        [NonAction]
        private void ConnectImagesToFurniture(int id, List<S3Image> images)
        {   
            foreach(S3Image img in images)
            {
                FurnitureImage image = new FurnitureImage()
                {
                    furnitureId = id,
                    s3ImageId = img.s3ImageId
                };
                _context.Add(image);
            }

            _context.SaveChanges();
        }
        
    }
}