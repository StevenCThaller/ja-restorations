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
        public async Task<IActionResult> GetAllFurniture()
        {
            try
            {
                List<Furniture> allFurniture = await _furnitureService.GetAllFurniture();
                return Ok(Json(new { message = "success", results = allFurniture }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [AllowAnonymous]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableFurniture()
        {
            try
            {
                List<Furniture> furniture = await _furnitureService.GetAvailableFurniture();
                return Ok(new { message = "success", results = furniture });
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateFurniture(NewFurnitureForm furnitureForm)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndRoleId(Request, 2))
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
                        height = (decimal)furnitureForm.height,
                        length = (decimal)furnitureForm.length,
                        width = (decimal)furnitureForm.width,
                        estimatedWeight = (int)furnitureForm.estimatedWeight
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
        public async Task<IActionResult> DeleteFurniture(int furnitureId)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndRoleId(Request, 2))
                {
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }

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

                return Ok(Json(new { message = "success", data = existing }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message}));
            }
        }
        
        [AllowAnonymous]
        [HttpGet("types")]
        public JsonResult GetTypes()
        {
            return Json(new { types = _context.FurnitureTypes }); 
        }

        [HttpPatch("sell")]
        public async Task<IActionResult> MarkFurnitureAsSold([FromBody] SaleForm saleForm)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndRoleId(Request, 2))
                {
                    System.Console.WriteLine("Ew go away.");
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }

                Sale justAdded = await _furnitureService.MarkAsSold(saleForm);

                if(justAdded == null) 
                {
                    return BadRequest(Json(new { message = "error", results = "Unable to add" }));
                }
                
                return Ok(Json(new { message = "success", results = justAdded }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpPost("like")]
        public async Task<IActionResult> AddLike([FromBody] FurnitureLikeForm furnitureLikeForm)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndUserId(Request, furnitureLikeForm.userId))
                {
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }
                FurnitureLike newlyAdded = await _furnitureService.AddLike(furnitureLikeForm);

                if(newlyAdded == null)
                {
                    return BadRequest(Json(new { message = "error", results = "Unable to add like." }));
                }

                return Ok(Json(new { message = "success", results = newlyAdded }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message}));
            }
        }

        [HttpDelete("{furnitureLikeId}/unlike")]
        public async Task<IActionResult> RemoveLike(int furnitureLikeId)
        {
            try
            {
                int userId = await _authService.GetUserIdFromHeaders(Request);
                if(userId == -1)
                {
                    return Unauthorized(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource"}));
                }

                if(!await _furnitureService.RemoveLike(furnitureLikeId))
                {
                    return BadRequest(Json(new { message = "error", results = false }));
                }

                return Ok(Json(new { message = "success", results = true }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json( new { message = "error", results = ex.Message }));
            }
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