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
using System.Web;
using System.Linq;

namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    [Route("api/[controller]")]
    public class UsersController : Controller 
    {
        private IUserService _userService;
        private IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndRoleId(Request, 1))
                {
                    return BadRequest(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource" }));
                }

                User user = await _userService.GetUser(userId);

                return Ok(Json(new { message = "success", results = user }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpGet("{userId}/account")]
        public async Task<IActionResult> GetAccountDetails(int userId)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndUserId(Request, userId))
                {
                    return BadRequest(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource" }));
                }

                UserDetails user = await _userService.GetUserDetails(userId);

                return Ok(Json(new { message = "success", results = user }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpPut("{userId}/edit")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDetails viewUser)
        {
            try
            {
                if(!await _authService.AuthorizeByHeadersAndUserId(Request, 1))
                {
                    return BadRequest(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource" }));
                }

                User updated = await _userService.UpdateUser(userId, viewUser);
                
                return Ok(Json(new { message = "success", results = updated }));
            }
            catch(Exception ex) 
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }

        [HttpGet("{userId}/verify")]
        public async Task<IActionResult> VerifyUser(int userId)
        {
            try
            {
                if(!await _authService.AuthorizeByHeaders(Request))
                {
                    return BadRequest(Json(new { message = "unauthorized", results = "You are unauthorized to access this resource" }));
                }

                return Ok(Json(new { message = "success", results = true }));
            }
            catch(Exception ex)
            {
                return BadRequest(Json(new { message = "error", results = ex.Message }));
            }
        }
    }
}