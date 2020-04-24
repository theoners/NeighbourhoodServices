namespace NeighbourhoodServices.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Common;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Web.Infrastructure;
    using NeighbourhoodServices.Web.ViewModels;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Categories;
    using NeighbourhoodServices.Web.ViewModels.Home;
    using NeighbourhoodServices.Web.ViewModels.Users;

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

        public IActionResult Test(int? page)
        {
            return this.View();
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories = this.categoriesService.GetAll<IndexCategoriesView>(),
                AspNetUsersCount = this.userService.GetUserCount(),
                Announcements = this.announcementService.GetByCreatedOn<AnnouncementViewModel>(),
                TopUsers = this.userService.GetTopUsers<TopUserViewModel>(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        [Route("Обяви/{currentPage?}")]
        public IActionResult AllAnnouncements(int currentPage = 1)
        {
            var skip = (currentPage - 1) * AnnouncementsConstants.AnnouncementsPerPage;
            var pageModel = new Page
            {
                CurrentPage = currentPage,
            };
            var totalAnnouncement = this.announcementService.AllAnnouncementCount();
            var announcementViewModel = this.announcementService.GetByCreatedOn<AnnouncementViewModel>(skip);
            var categoriesViewModel = this.categoriesService.GetAll<IndexCategoriesView>();

            pageModel.AnnouncementsCount = totalAnnouncement;
            var viewModel = new GetAllViewModel()
            {
                Announcements = announcementViewModel,
                Categories = categoriesViewModel,
                Page = pageModel,
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
