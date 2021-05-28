using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.Models;
using System.Threading.Tasks;

namespace backend.Helpers{
    public class IsEmployeeAuthorizationHandler : AuthorizationHandler<IsEmployeeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsEmployeeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                // context.Succeed(requirement);
                return Task.FromResult(0);
            }

            var role = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value);

            if(role == 2)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}