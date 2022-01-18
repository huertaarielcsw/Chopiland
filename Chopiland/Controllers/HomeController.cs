using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Chopiland.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        
        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
       
        public IActionResult Index()
        {
            //if(User.Identity.IsAuthenticated)
            //{
            //    await roleManager.CreateAsync(new IdentityRole("Admin"));
            //    var user = await userManager.GetUserAsync(HttpContext.User);
            //    await userManager.AddToRoleAsync(user, "Admin");
            //}
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
