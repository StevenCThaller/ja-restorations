using Microsoft.AspNetCore.Authorization;
using backend.Models;
using System.Threading.Tasks;

namespace backend.Helpers{
    public class IsUserHandler : AuthorizationHandler<IsUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsUserRequirement requirement)
        {
            
            if (context.User.IsInRole("User"))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}