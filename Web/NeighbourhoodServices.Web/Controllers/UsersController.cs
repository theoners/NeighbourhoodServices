namespace NeighbourhoodServices.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels.Users;

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
