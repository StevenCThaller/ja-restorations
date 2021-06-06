using System;
using System.Linq;
using System.Collections.Generic;


namespace backend.Models 
{
    public class UserDetails
    {
        public UserDetails(){}
        public UserDetails(User user)
        {
            firstName = user.firstName;
            lastName = user.lastName;
            email = user.email;
            profilePicture = user.profilePicture;
            likedFurniture = user.likedPieces
                                .Select(l => l.furniture)
                                .ToList();
            appraisals = user.appraisals;
            createdAt = user.createdAt;
        }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string profilePicture { get; set; }
        public List<Furniture> likedFurniture { get; set; }
        public List<Appraisal> appraisals { get; set; }
        // public List<RefurbishRequest> RefurbishRequests { get; set; }
        public DateTime createdAt = DateTime.Now;
    }
}