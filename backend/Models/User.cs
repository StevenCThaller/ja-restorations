using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string oauthSubject { get; set; }
        public string oauthIssuer { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }

    public class UserView
    {
        public string tokenId { get; set; }
    }

    public class SubToken
    {
        public string token { get; set; }
    }
}