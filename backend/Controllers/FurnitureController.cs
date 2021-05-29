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
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using backend.Helpers;

using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    [ApiController]
    [Route("api/[controller]")]
    public class FurnitureController : Controller 
    {
        private IAuthService _authService;

        // public FurnitureController(IAuthService authService)
        // {
        //     _authService = authService;
        // }

        private IFurnitureService _furnitureService;
        private MyContext _context;

        public FurnitureController(MyContext context, IAuthService authService, IFurnitureService furnitureService)
        {
            _authService = authService;
            _context = context;
            _furnitureService = furnitureService;
        }

        [AllowAnonymous]
        [HttpGet("")]        
        public JsonResult GetFurniture()
        {
            IEnumerable<Furniture> allFurniture = _context.Furniture
                .Include(f => f.type)
                .Include(f => f.colors)
                .Include(f => f.images)
                .ThenInclude(i => i.s3Image);

            return Json(new { message = "success", data = allFurniture });
        }

        [AllowAnonymous]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableFurniture()
        {
            try
            {
                await Task.Delay(1);
                List<Furniture> furniture = _furnitureService.GetAvailableFurniture();
                return Ok(new { message = "success", results = furniture });
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("fucking how?");
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateFurniture(NewFurnitureForm furnitureForm)
        {
            try
            {
                if(!await _authService.AuthorizeByHeaders(Request, 2))
                {
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }

                if(ModelState.IsValid)
                {
                    FurnitureType existingType = _context.FurnitureTypes.FirstOrDefault(t => t.name.ToLower() == furnitureForm.type.ToLower());
                    if(existingType == null)
                    {
                        existingType = new FurnitureType()
                        {
                            name = furnitureForm.type.ToLower()
                        };
                        _context.Add(existingType);
                    }

                    Furniture furniture = new Furniture()
                    {
                        name = furnitureForm.name,
                        description = furnitureForm.description,
                        type = existingType,
                        priceFloor = furnitureForm.priceFloor,
                        priceCeiling = furnitureForm.priceCeiling,
                        height = furnitureForm.height,
                        length = furnitureForm.length,
                        width = furnitureForm.width,
                        estimatedWeight = furnitureForm.estimatedWeight
                    };

                    _context.Add(furniture);                
                    _context.SaveChanges();
                    return Ok(Json(new { message = "success", results = furniture.furnitureId }));
                } else {
                    return BadRequest(Json(new { message = "error", results = ModelState.Values }));
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpDelete("{furnitureId}/delete")]
        public async Task<JsonResult> DeleteFurniture(int furnitureId)
        {
            await Task.Delay(1);
            Furniture existing = _context.Furniture
                                    .Include(f => f.images)
                                    .ThenInclude(i => i.s3Image)
                                    .FirstOrDefault(f => f.furnitureId == furnitureId);
            if(existing == null)
            {
                return Json(new { message = "error", error = "Furniture does not exist." });
            }
            if(existing.images.Count > 0)
            {
                IEnumerable<S3Image> imagesToDelete = existing.images
                                                        .Where(i => _context.AppraisalImages.All(ai => ai.s3ImageId != i.s3ImageId))
                                                        .Select(i => i.s3Image);

                ImageService.DeleteImages(imagesToDelete);
                foreach(S3Image img in imagesToDelete)
                {
                    _context.Remove(img);
                }
            }
            _context.Remove(existing);
            _context.SaveChanges();

            return Json(new { message = "success", data = existing });
        }
        
        [AllowAnonymous]
        [HttpGet("types")]
        public JsonResult GetTypes()
        {
            return Json(new { types = _context.FurnitureTypes }); 
        }


        // [HttpPost("type")]
        // public JsonResult AddType(TypeView typeView)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         FurnitureType type = _context.FurnitureTypes.FirstOrDefault(t => t.name == typeView.name.ToLower());
        //         if(type == null)
        //         {
        //             type = new FurnitureType()
        //             {
        //                 name = typeView.name.ToLower()
        //             };

        //             _context.Add(type);
        //             _context.SaveChanges();                    
        //         }

        //         return Json(new { message = "success", data = type.typeId });
        //     } else if(!ModelState.IsValid) {
        //         return Json(new { message = "error", data = ModelState.Values });
        //     } else {
        //         return Json(new { message = "error", data = BadRequest() });
        //     }
        // }
        
        
    }
}