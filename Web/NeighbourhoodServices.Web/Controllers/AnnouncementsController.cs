using NeighbourhoodServices.Common;
using NeighbourhoodServices.Web.Infrastructure;

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
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    [Authorize]
    public class AnnouncementsController : BaseController
    {
        private readonly IAnnouncementService announcementService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;

        public AnnouncementsController(IAnnouncementService announcementService, UserManager<ApplicationUser> userManager, ICategoriesService categoriesService)
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
            var announcementViewModel = this.announcementService.GetDetails<AnnouncementDetails>(id);
            return this.View(announcementViewModel);
        }

        [Route("МоитеОбяви/{currentPage?}")]
        public IActionResult GetByUser(string id, int currentPage = 1)
        {

            var skip = (currentPage - 1) * AnnouncementsConstants.AnnouncementsPerPage;
            var pageModel = new Page
            {
                CurrentPage = currentPage,
            };
            var announcementViewModel = this.announcementService.GetByUser<AnnouncementViewModel>(id);
            var viewModel = new GetAllViewModel()
            {
                Announcements = announcementViewModel,
                Page = pageModel,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.announcementService.DeleteAsync(id);
            return this.Redirect("/");
        }

        public IActionResult GetUpdateView(string id)
        {
            var announcement = this.announcementService.GetDetails<AnnouncementViewModel>(id);
            var categories = this.categoriesService.GetAll<AnnouncementCategoriesView>();
            var viewModel = new AnnouncementUpdateModel()
            {
                Categories = categories,
                Announcement = announcement,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Update(AnnouncementInputModel announcementInputModel, string id)
        {
            await this.announcementService.UpdateAsync(announcementInputModel, id);
            return this.RedirectToAction("GetDetails", new { id = id });
        }
    }
}
