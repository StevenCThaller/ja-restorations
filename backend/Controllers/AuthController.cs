using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Models.Auth;
using backend.Helpers;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : Controller 
    {
        private IGoogleAuthService _authService;
        // private IUserService _userService;
        public AuthController(IGoogleAuthService authService /*, IUserService userService*/)
        {
            _authService = authService;
            // _userService = userService;
        }

        // public AuthController(IAuthService authService)
        // {
        //     _authService = authService;
        // }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> GoogleLoginOrRegister([FromBody]GoogleLogin gLogin)
        {
            try 
            {
                System.Console.WriteLine("userView = " + gLogin.tokenId);
                var payload = GoogleJsonWebSignature.ValidateAsync(gLogin.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                AuthResponse response = await _authService.Authenticate(payload, ipAddress());
                // System.Console.WriteLine(user);
                // string role;
                // if()

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(AppSettings.appSettings.JwtEmailEncryption, response.email)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Role", response.roleId.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.appSettings.JwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(String.Empty,
                    String.Empty,
                    claims,
                    expires: DateTime.Now.AddSeconds(55*60),
                    signingCredentials: creds);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                // System.Console.WriteLine("You goofd");
                // System.Console.WriteLine(ex);
                BadRequest(ex.Message);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("denied")]
        public async Task<IActionResult> Denied()
        {
            await Task.Delay(0);
            return Json(new { message = "denied", results = "Not authorized" });
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}