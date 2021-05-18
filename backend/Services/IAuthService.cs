using backend.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
    }

    public class AuthService : IAuthService
    {
        private MyContext _context;
        public AuthService(MyContext context)
        {
            _context = context;
        }
        public async Task<User> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        {
            await Task.Delay(1);
            return this.FindUserOrAdd(payload);
        }

        private User FindUserOrAdd(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
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
                    oauthSubject = payload.Subject,
                    oauthIssuer = payload.Issuer,
                };
                _context.Add(u);

                if(payload.Email == AppSettings.appSettings.SuperAdmin)
                {
                    Administrator admin = new Administrator()
                    {
                        user = u
                    };
                    _context.Add(admin);
                }

                _context.SaveChanges();
            }

            return u;
        }
    }
}