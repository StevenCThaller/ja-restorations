using System;
using System.Collections.Generic;
using System.Linq;
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


namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : Controller 
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // public AuthController(IAuthService authService)
        // {
        //     _authService = authService;
        // }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody]UserView userView)
        {
            try 
            {
                System.Console.WriteLine("userView = " + userView.tokenId);
                var payload = GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                var user = await _authService.Authenticate(payload);
                // System.Console.WriteLine(user);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(AppSettings.appSettings.JwtEmailEncryption, user.email)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
    }
}