using System.Linq;

namespace NeighbourhoodServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Common;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Web.Infrastructure;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Categories;
    using NeighbourhoodServices.Web.ViewModels.Comments;

    [Authorize]
    public class AnnouncementsController : BaseController
    {
        private readonly IAnnouncementService announcementService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly ICommentService commentService;

        public AnnouncementsController(IAnnouncementService announcementService, UserManager<ApplicationUser> userManager, ICategoriesService categoriesService, ICommentService commentService)
        {
            this.announcementService = announcementService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.commentService = commentService;
        }

       
        [Route("ПубликувайОбява")]
        public IActionResult GetCreateView(AnnouncementInputModel announcementInputModel)
        {
            var categories = this.categoriesService.GetAll<AnnouncementCategoriesView>();
            var createModel = new CreateModel()
            {
               Categories = categories,
               Announcement = announcementInputModel,
            }; 
            return this.View(createModel);
        }

        [HttpPost]
       
        public async Task<IActionResult> Create(AnnouncementInputModel announcementInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.categoriesService.GetAll<AnnouncementCategoriesView>();
              
                var createModel = new CreateModel()
                {
                    Categories = categories,
                    Announcement = announcementInputModel,
                };


                return this.View("GetCreateView", createModel);

            }

            var user = await this.userManager.GetUserAsync(this.User);
           var id = await this.announcementService.CreateAsync(announcementInputModel, user.Id);

           return this.RedirectToAction("GetDetails", new { id = id });
        }

        [Route("Обява/{id}")]
        public IActionResult GetDetails(string id)
        {
            var announcementViewModel = this.announcementService.GetDetails<AnnouncementDetails>(id);
            if (announcementViewModel == null)
            {
                return this.Redirect("/");
            }

            var comments = this.commentService.GetCommentByPostId<CommentViewModel>(id);
            announcementViewModel.Comments = comments;
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
            var currentUrl = this.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Referer").Value;
            await this.announcementService.DeleteAsync(id);
            return this.Redirect(currentUrl);
        }

        [HttpGet]
        [Route("Редактирай/{id?}")]
        public IActionResult GetUpdateView(string id)
        {
            var announcement = this.announcementService.GetDetails<AnnouncementViewModel>(id);
            if (announcement == null)
            {
                return this.Redirect("/");
            }

            var categories = this.categoriesService.GetAll<AnnouncementCategoriesView>();
            var viewModel = new AnnouncementUpdateModel()
            {
                Categories = categories,
                Announcement = announcement,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Редактирай/{id?}")]
        public async Task<IActionResult> Update(AnnouncementInputModel announcementInputModel, string id)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.categoriesService.GetAll<AnnouncementCategoriesView>();
                var currentAnnouncement = new AnnouncementViewModel()
                {
                    Id = id,
                    Title = announcementInputModel.Title,
                    ServiceType = announcementInputModel.ServiceType,
                    CategoryName = announcementInputModel.Category,
                    Description = announcementInputModel.Description,
                    Price = announcementInputModel.Price,
                    Place = announcementInputModel.Place,
                };
                var viewModel = new AnnouncementUpdateModel()
                {
                    Categories = categories,
                    Announcement = currentAnnouncement,
                };
                return this.View("GetUpdateView", viewModel);
            }

            await this.announcementService.UpdateAsync(announcementInputModel, id);
            return this.RedirectToAction("GetDetails", new { id = id });
        }

        public IActionResult Search(string search, string category, string city, int currentPage = 1)
        {
            var skip = (currentPage - 1) * AnnouncementsConstants.AnnouncementsPerPage;
            var pageModel = new Page
            {
                CurrentPage = currentPage,
            };
            var announcementViewModel = this.announcementService.GetByKeyWord<AnnouncementViewModel>(search, category, city);
            var viewModel = new GetAllViewModel()
            {
                Announcements = announcementViewModel,
                Page = pageModel,
            };
            return this.View(viewModel);
        }
    }
}
