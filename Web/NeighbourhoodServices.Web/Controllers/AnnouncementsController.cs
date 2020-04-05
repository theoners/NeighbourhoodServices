namespace NeighbourhoodServices.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Web.ViewModels.Announcement;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    [Authorize]
    public class AnnouncementsController : BaseController
    {
        private readonly IAnnouncementService announcementService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;

        public AnnouncementsController(IAnnouncementService announcementService, UserManager<ApplicationUser> userManager , ICategoriesService categoriesService)
        {
            this.announcementService = announcementService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
        }

        [Route("ПубликувайОбява")]
        public IActionResult GetCreateView()
        {
            var viewModel = this.categoriesService.GetAll<AnnouncementCategoriesView>();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AnnouncementInputModel announcementInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.announcementService.CreateAsync(announcementInputModel, user.Id);

            return this.Redirect("/");
        }

        [Route("Обява/{id}")]
        public IActionResult GetDetails(string id)
        {
            var announcementViewModel = this.announcementService.GetDetails<AnnouncementViewModel>(id);
            var categoriesViewModel = this.categoriesService.GetAll<IndexCategoriesView>();

            var viewModel = new GetAllViewModel()
            {
                Announcements = new List<AnnouncementViewModel>(),
                Categories = categoriesViewModel,

            };

            return this.View(viewModel);
        }

        public IActionResult GetByUser()
        {
            var userId = this.userManager.GetUserId(this.User);
            var announcementViewModel = this.announcementService.GetByUser<AnnouncementViewModel>(userId);
            var viewModel = new GetAllViewModel()
            {
                Announcements = announcementViewModel,
            };
            return this.View(viewModel);
        }
    }
}
