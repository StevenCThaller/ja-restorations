using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.Models;
using System.Threading.Tasks;

namespace backend.Helpers{
    public class IsUserAuthorizationHandler : AuthorizationHandler<IsUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsUserRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                // context.Succeed(requirement);
                return Task.FromResult(0);
            }

            var role = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value);

            if(role >= 1)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}