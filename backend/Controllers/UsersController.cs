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
                if(!await _authService.AuthorizeByHeaders(Request, 1))
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
    }
}