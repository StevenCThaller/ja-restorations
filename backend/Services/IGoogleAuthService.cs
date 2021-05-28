using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Google.Apis.Auth;
using backend.Helpers;


namespace backend.Services
{
    public interface IGoogleAuthService
    {
        Task<AuthResponse> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload, string ipAddress);
    }

    public class GoogleAuthService : IGoogleAuthService
    {
        private MyContext _context;
        public GoogleAuthService(MyContext context)
        {
            _context = context;
        }
        public async Task<AuthResponse> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload, string ipAddress)
        {
            await Task.Delay(1);
            var user = this.FindUserOrAdd(payload, ipAddress);

            string jwtToken = generateJwtToken(user);
            RefreshToken refreshToken = generateRefreshToken(ipAddress);

            refreshToken.userId = user.userId;

            _context.Add(refreshToken);
            _context.SaveChanges();

            return new AuthResponse(user, jwtToken, refreshToken.token);
        }


        private User FindUserOrAdd(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload, string ipAddress)
        {
            var u = _context.Users.FirstOrDefault(u => u.email == payload.Email);
            System.Console.WriteLine(payload.Email);
            if(u == null)
            {
                u = new User()
                {
                    firstName = payload.GivenName,
                    lastName = payload.FamilyName,
                    email = payload.Email,
                    roleId = payload.Email == AppSettings.appSettings.SuperAdmin ? 3 : 1,
                    oauthSubject = payload.Subject,
                    oauthIssuer = payload.Issuer,
                };
                _context.Add(u);

                _context.SaveChanges();
            }
            return u;
            
    
        }

        public AuthResponse RefreshToken(string token, string ipAddress)
        {
            var user = _context.Users.Include(u => u.refreshTokens).FirstOrDefault(u => u.refreshTokens.Any(t => t.token == token));
            
            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.refreshTokens.Single(x => x.token == token);

            // return null if token is no longer active
            if (!refreshToken.isActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.revoked = DateTime.UtcNow;
            refreshToken.revokedByIp = ipAddress;
            refreshToken.replacedByToken = newRefreshToken.token;
            user.refreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = generateJwtToken(user);

            return new AuthResponse(user, jwtToken, newRefreshToken.token);
        }

        private string generateJwtToken(User user)
        {
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(new Claim[] 
            //     {
            //         new Claim(ClaimTypes.Name, user.userId.ToString())
            //     }),
            //     Expires = DateTime.UtcNow.AddMinutes(15),
            //     Role = new ClaimsIdentity(new Claim[]
            //     {
            //         new Claim("Role", user.roleId.ToString())
            //     })
            //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            // };
            // var token = tokenHandler.CreateToken(tokenDescriptor);
            // return tokenHandler.WriteToken(token);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(AppSettings.appSettings.JwtEmailEncryption, user.email)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", user.roleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.appSettings.JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(String.Empty,
                String.Empty,
                claims,
                expires: DateTime.Now.AddSeconds(55*60),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using(var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    token = Convert.ToBase64String(randomBytes),
                    expirationDate = DateTime.UtcNow.AddDays(7),
                    createdByIp = ipAddress
                };
            }
        }
    }
}