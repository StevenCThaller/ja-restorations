using Microsoft.AspNetCore.Authorization;


namespace backend.Models
{
    public class IsUserRequirement : IAuthorizationRequirement
    {
        public IsUserRequirement(){}
    }
}