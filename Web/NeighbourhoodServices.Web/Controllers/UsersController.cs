namespace NeighbourhoodServices.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Web.ViewModels.Comments;
    using NeighbourhoodServices.Web.ViewModels.Users;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IUsersService userService;
        private readonly ICommentService commentService;

        public UsersController(UserManager<ApplicationUser> userManager, IRepository<ApplicationUser> userRepository, IUsersService userService, ICommentService commentService)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.userService = userService;
            this.commentService = commentService;
        }

        [Authorize]
        public IActionResult UserProfile(string id)
        {
            var user = this.userService.GetUser<UserViewModel>(id);
            user.Comments = this.commentService.GetCommentByUserId<CommentViewModel>(id).ToList();
            return this.View(user);
        }

        [Authorize]
        public IActionResult SearchUser(string username, string city)
        {
            var user = this.userService.SearchUser<UserViewModel>(username, city);
            if (user == null)
            {
                return this.View("NotFound");
            }

            user.Comments = this.commentService.GetCommentByUserId<CommentViewModel>(user.Id).ToList();
            return this.View("UserProfile", user);
        }
    }
}
