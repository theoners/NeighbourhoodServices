using System.Linq;
using NeighbourhoodServices.Services.Data.Interface;
using NeighbourhoodServices.Web.ViewModels.Comments;

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
        private readonly ICommentService commentService;

        public UsersController(UserManager<ApplicationUser> userManager, IRepository<ApplicationUser> userRepository, IUsersService userService, ICommentService commentService)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.userService = userService;
            this.commentService = commentService;
        }

        public IActionResult UserProfile(string id)
        {
            var user = this.userService.GetUser<UserViewModel>(id);
            user.Comments = this.commentService.GetCommentByUserId<CommentViewModel>(id).ToList();
            return this.View(user);
        }
    }
}
