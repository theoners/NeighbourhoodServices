using NeighbourhoodServices.Web.ViewModels;

namespace NeighbourhoodServices.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels.Announcement;

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

        public IActionResult GetAll()
        {
            var viewModel = this.announcementService.GetByCreatedOn<AnnouncementViewModel>();
            return this.View(viewModel);
        }

        public IActionResult GetByCategory(string name)
        {
            var viewModel = this.announcementService.GetByCategory<ServiceViewModel>(name);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = this.categoriesService.GetAll<AnnouncementCategoriesView>();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(AnnouncementInputModel announcementInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.announcementService.CreateAsync(announcementInputModel, user.Id);

            return this.Redirect("/");
        }
    }
}
