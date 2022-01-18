﻿using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Authorization
{
    public class IsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, BaseEntity>
    {
        UserManager<User> _userManager;
        public IsOwnerAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, BaseEntity resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }
            // If not asking for CRUD permission, return.
            if (requirement.Name != Constants.CreateOperationName &&
            requirement.Name != Constants.ReadOperationName &&
            requirement.Name != Constants.UpdateOperationName &&
            requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }
            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }  
}
