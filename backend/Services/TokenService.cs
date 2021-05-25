// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using backend.Models;
// using Microsoft.IdentityModel.Tokens;

// public interface ITokenService
// {
//     string GenerateAccessToken(IEnumerable<Claim> claims);
//     string GenerateRefreshToken();
//     ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
// }

// public class TokenService : ITokenService
// {
//     public string GenerateAccessToken(IEnumerable<Claim> claims)
//     {
//         SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.appSettings.JwtSecret));
//         SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//         JwtSecurityToken tokenOptions = new JwtSecurityToken(
//             issuer: "http://localhost:5000",
//             audience: "http://localhost:5000",
//             claims: claims,
//             expires: DateTime.Now.AddMinutes(5),
//             signingCredentials: signingCredentials
//         );
        
//     }
// }