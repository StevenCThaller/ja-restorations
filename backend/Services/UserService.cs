using System;
using System.Collections.Generic;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using backend.Helpers;

namespace backend.Services
{
    public interface IUserService
    {
        
        Task<bool> AuthorizeById(int userId, int roleId);
        Task<bool> AuthorizeByEmail(string email, int roleId);
        Task<User> GetUser(int id);
    }

    public class UserService : IUserService
    {
        private MyContext _context;

        public UserService(MyContext context) 
        {
            _context = context;
        }

        

        public async Task<bool> AuthorizeById(int userId, int roleId)
        {
            User user = await this.GetUser(userId);
        
            return user.roleId >= roleId;
        }
        public async Task<bool> AuthorizeByEmail(string email, int roleId)
        {
            return _context.Users.Any(u => u.email == email && u.roleId == roleId);
        }

        public async Task<User> GetUser(int id)
        {
            await Task.Delay(0);
            return _context.Users.FirstOrDefault(u => u.userId == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            await Task.Delay(0);
            return _context.Users;
        }

        // public async Task<User> CreateUser()
    }
}