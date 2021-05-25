using Newtonsoft.Json;

namespace backend.Models.Auth
{
    public class AuthResponse
    {
        public string JwtToken {get; set;}

        [JsonIgnore] // not adding to response body, will put in http only cookie later
        public string RefreshToken {get; set;}
        // can add other fields
        // public int UserId {get; set;}

        // public AuthResponse(string jwtToken, string refreshToken)
        // {
        //     JwtToken = jwtToken;
        //     RefreshToken = refreshToken;
        // }
    }
}