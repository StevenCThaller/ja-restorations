using Microsoft.AspNetCore.Authorization;
using backend.Models;
using System.Threading.Tasks;

namespace backend.Helpers{
    public class IsAdminAuthorizationHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}