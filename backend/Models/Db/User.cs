using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Auth;
using Newtonsoft.Json;

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
        public string profilePicture { get; set; }
        [ForeignKey("Role")]
        public int roleId { get; set; }
        public Role role { get; set; }
        public List<RefreshToken> refreshTokens {get; set;}
        public List<FurnitureLike> likedPieces { get; set; }
        public List<Appraisal> appraisals { get; set; }


        // public List<RefurbishRequest> RefurbishRequests { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }

}