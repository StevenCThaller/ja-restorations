// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using backend.Models;
// using backend.Helpers;
// using backend.Services;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Google.Apis.Auth;
// using System.Security.Claims;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;

// namespace backend.Controllers
// {
//     // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
//     [Route("api/[controller]")]
//     public class UserController : Controller 
//     {
//         private IAuthService _authService;

//         public UserController(IAuthService authService)
//         {
//             _authService = authService;
//         }

//         // [AllowAnonymous]
//         [HttpGet("getdata")]
//         public void GetData([FromHeader])
//         {
//             try
//             {
//                 string bearerToken;
                
//                 // return RedirectToAction("Google", "Auth");
//             }
//             catch (Exception ex)
//             {
//                 // BadRequest(ex.Message);
//                 System.Console.WriteLine("uh oh");
//             }
//             // return BadRequest();
//         }
//     }
// }