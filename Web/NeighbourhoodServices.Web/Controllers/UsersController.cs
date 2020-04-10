using System.Linq;
using System.Net.Mime;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using NeighbourhoodServices.Common;
using NeighbourhoodServices.Data.Common.Repositories;
using NeighbourhoodServices.Services.Data;
using NeighbourhoodServices.Web.ViewModels.Users;

namespace NeighbourhoodServices.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Web.Areas.Identity.Pages.Account;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IUsersService userService;

        public UsersController(UserManager<ApplicationUser> userManager, IRepository<ApplicationUser> userRepository, IUsersService userService)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.userService = userService;
        }

        public IActionResult UserProfile(string id)
        {
            var user = this.userService.GetUser<UserViewModel>(id);
            return this.View(user);
        }
    }
}
