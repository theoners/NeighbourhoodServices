using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Web.Areas.Identity.Pages.Account;

namespace NeighbourhoodServices.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LoginModel> logger;


        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public async Task<IActionResult> Login(string userName, string password)
        {

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await this.userManager.Users
                    .FirstOrDefaultAsync(u => u.UserName == userName);
                if (user != null)
                {
                    var result = await this.signInManager.PasswordSignInAsync(user.UserName, password, true, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        this.logger.LogInformation("User logged in.");
                        return this.LocalRedirect("/");
                    }

                    //else
                    //{
                    //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    //    return this.PartialView("_LoginRegisterPopUp", "false");
                    //}
                }
            }



            return this.Redirect("/");
        }
        public async Task<IActionResult> Register(string userName, string password, string email)
        {

            var result = await this.userManager.CreateAsync(
                new ApplicationUser()
                {
                    UserName = userName,
                    Email = email,

                }, password);

            return this.Redirect("/");
        }
    }
}
