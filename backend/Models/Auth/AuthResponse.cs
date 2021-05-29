using Newtonsoft.Json;

namespace backend.Models
{
    public class AuthResponse
    {
        public int userId { get; set; }
        public string email { get; set; }
        public int roleId { get; set; }
        public string jwtToken {get; set;}

        [JsonIgnore] // not adding to response body, will put in http only cookie later
        public string refreshToken {get; set;}

        public AuthResponse(User user, string jwtToken, string refreshToken)
        {
            userId = user.userId;
            email = user.email;
            roleId = user.roleId;
            this.jwtToken = jwtToken;
            this.refreshToken = refreshToken;
        }
        // can add other fields
        // public int UserId {get; set;}

        // public AuthResponse(string jwtToken, string refreshToken)
        // {
        //     JwtToken = jwtToken;
        //     RefreshToken = refreshToken;
        // }
    }
}