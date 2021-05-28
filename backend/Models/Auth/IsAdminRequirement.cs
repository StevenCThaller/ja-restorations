using Microsoft.AspNetCore.Authorization;


namespace backend.Models
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
        public IsAdminRequirement(){}
    }
}