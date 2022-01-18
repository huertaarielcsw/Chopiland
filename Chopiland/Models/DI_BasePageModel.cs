using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class DI_BasePageModel
    {
        protected ApplicationContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<User> UserManager { get; }
        public DI_BasePageModel(ApplicationContext context, IAuthorizationService authorizationService, UserManager<User> userManager)
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}
