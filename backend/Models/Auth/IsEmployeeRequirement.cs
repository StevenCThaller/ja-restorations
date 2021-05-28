using Microsoft.AspNetCore.Authorization;


namespace backend.Models
{
    public class IsEmployeeRequirement : IAuthorizationRequirement
    {
        public IsEmployeeRequirement(){}
    }
}