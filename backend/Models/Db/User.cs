using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string oauthSubject { get; set; }
        public string oauthIssuer { get; set; }
        [ForeignKey("Role")]
        public int roleId { get; set; }
        public Role role { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
        public List<RefreshToken> refreshTokens {get; set;}
    }

}