using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Web.Areas.Identity.Pages.Account;

namespace NeighbourhoodServices.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;


        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Login(string userName, string password, string email)
        {

            var result = await this.signInManager.PasswordSignInAsync(userName, password, true, true);
               

            return this.Redirect("/");
        }
        public async Task<IActionResult> Register(string userName, string password, string email)
        {

            var result= await this.userManager.CreateAsync(
                new ApplicationUser()
            {
                UserName = userName,
                Email = email,

            }, password);

            return this.Redirect("/");
        }
    }
}
