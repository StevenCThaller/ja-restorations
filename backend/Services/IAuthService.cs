using System;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using backend.Helpers;
using Microsoft.AspNetCore.Http;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<bool> AuthorizeByHeadersAndRoleId(HttpRequest request, int clearance);
        Task<bool> AuthorizeByHeadersAndUserId(HttpRequest request, int userId);
        Task<bool> AuthorizeByHeaders(HttpRequest request);

        Task<int> GetUserIdFromHeaders(HttpRequest request);
        // AuthResponse AuthenticateWithProvider(AuthProvider provider, IAuthRequest authRequest);
        // // AuthResponse AuthenticateWithFacebook(IAuthRequest model);
        // // AuthResponse AuthenticateWithTwitter(IAuthRequest model);
        // AuthResponse RefreshToken(string token);
        // bool RevokeToken(string token);
        // User GetById(int id);
    }

    public class AuthService : IAuthService
    {
        private MyContext _context;
        public AuthService(MyContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorizeByHeadersAndRoleId(HttpRequest request, int clearance)
        {
            await Task.Delay(0);
            var something = request.Headers["Authorization"].ToString().Remove(0, 7);

            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(something) as JwtSecurityToken;

            string decryptedEmail = Security.Decrypt(AppSettings.appSettings.JwtEmailEncryption, token.Claims.First(c => c.Type == "sub").Value);
            var roleResult = Int32.TryParse(token.Claims.First(c => c.Type == "Role").Value, out int roleId);

            if(decryptedEmail == null || !roleResult || !this.AuthorizeByEmailAndRole(decryptedEmail, roleId))
            {
                return false;
            }

            return true;
        }
        private bool AuthorizeByEmailAndRole(string email, int roleId)
        {
            return _context.Users.Any(u => u.email == email && u.roleId == roleId);
        }

        public async Task<bool> AuthorizeByHeadersAndUserId(HttpRequest request, int userId)
        {
            await Task.Delay(0);
            var something = request.Headers["Authorization"].ToString().Remove(0, 7);
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(something) as JwtSecurityToken;

            string decryptedEmail = Security.Decrypt(AppSettings.appSettings.JwtEmailEncryption, token.Claims.First(c => c.Type == "sub").Value);
            
            if(!_context.Users.Any(u => u.email == decryptedEmail && u.userId == userId))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AuthorizeByHeaders(HttpRequest request)
        {
            await Task.Delay(0);
            var something = request.Headers["Authorization"].ToString().Remove(0, 7);
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(something) as JwtSecurityToken;

            string decryptedEmail = Security.Decrypt(AppSettings.appSettings.JwtEmailEncryption, token.Claims.First(c => c.Type == "sub").Value);
            
            if(!_context.Users.Any(u => u.email == decryptedEmail))
            {
                return false;
            }
            return true;
        }

        public async Task<int> GetUserIdFromHeaders(HttpRequest request)
        {
            try 
            {
                await Task.Delay(0);
                var something = request.Headers["Authorization"].ToString().Remove(0,7);
                JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(something) as JwtSecurityToken;

                return Int32.Parse(token.Claims.First(c => c.Type == "UserId").Value);
            }
            catch
            {
                return -1;
            }

            
        }
        // public async Task<AuthResponse> AuthenticateWithProvider(AuthProvider provider, IAuthRequest authReq)
        // {

        //     switch (provider)
        //     {
        //         case AuthProvider.google:
        //             {
        //                 Google.Apis.Auth.GoogleJsonWebSignature.Payload authResult = AuthenticateWithGoogle(authReq);
        //                 break;
        //             }
        //         // case AuthProvider.facebook:
        //         // {
        //         //     var authResult = AuthenticateWithFacebook(authReq);
        //         //     break;
        //         // }
        //         // case AuthProvider.twitter:
        //         // {
        //         //     var authResult = AuthenticateWithTwitter(authReq);
        //         //     break;
        //         // }

        //         default: return new AuthResponse() { JwtToken = "", RefreshToken = "" };
        //     }
        //     await Task.Delay(1);
        //     // return this.FindUserOrAdd(payload);
        //     var response = new AuthResponse();
        //     return response;
        // }
        // private async Task<AuthResponse> AuthenticateWithGoogle(GoogleAuthRequest authReq)
        // {
        //     await Task.Delay(1);
        //     // return this.FindUserOrAdd(payload);
        //     var response = new AuthResponse();
        //     return response;
        // }
        // private async Task<AuthResponse> AuthenticateWithFacebook(FacebookAuthRequest authReq)
        // {
        //     await Task.Delay(1);
        //     //
        //     var response = new AuthResponse();
        //     return response;

        // }
        // private async Task<AuthResponse> AuthenticateWithTwitter(TwitterAuthRequest authReq)
        // {
        //     await Task.Delay(1);
        //     //
        //     var response = new AuthResponse();
        //     return response;

        // }
        // public AuthResponse RefreshToken(string token)
        // {
        //     var response = new AuthResponse();
        //     return response;
        // }
        // public bool RevokeToken(string token)
        // {
        //     return true;
        // }
        // public User GetById(int id)
        // {
        //     User u = _context.Users.Find(id);
        //     return u;
        // }
        // private User FindOrAdd(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        // {
        //     var u = _context.Users.FirstOrDefault(u => u.email == payload.Email);
        //     System.Console.WriteLine(payload.Email);
        //     if (u == null)
        //     {
        //         u = new User()
        //         {
        //             firstName = payload.GivenName,
        //             lastName = payload.FamilyName,
        //             email = payload.Email,
        //             oauthSubject = payload.Subject,
        //             oauthIssuer = payload.Issuer,
        //         };
        //         _context.Add(u);

        //         if (payload.Email == AppSettings.appSettings.SuperAdmin)
        //         {
        //             Administrator admin = new Administrator()
        //             {
        //                 user = u
        //             };
        //             _context.Add(admin);
        //         }

        //         _context.SaveChanges();
        //     }

        //     return u;
        // }
    }

}