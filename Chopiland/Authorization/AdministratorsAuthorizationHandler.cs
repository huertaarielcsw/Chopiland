using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Authorization
{
    public class AdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, BaseEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, BaseEntity resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            // Administrators can do anything.
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

    }
}
