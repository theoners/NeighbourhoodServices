using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace NeighbourhoodServices.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels;
    using NeighbourhoodServices.Web.ViewModels.Administration.Dashboard;
    using NeighbourhoodServices.Web.ViewModels.Announcement;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IUsersService userService;
        private readonly IAnnouncementService announcementService;

        public HomeController(ICategoriesService categoriesService, IUsersService userService, IAnnouncementService announcementService)
        {
            this.categoriesService = categoriesService;
            this.userService = userService;
            this.announcementService = announcementService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories = this.categoriesService.GetAll<IndexCategoriesView>(),
                AspNetUsersCount = this.userService.GetUserCount(),
                Announcement = this.announcementService.GetByCreatedOn<AnnouncementViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        
    }
}
