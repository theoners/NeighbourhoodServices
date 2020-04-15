using NeighbourhoodServices.Services.Data;
using NeighbourhoodServices.Web.ViewModels.Categories;

namespace NeighbourhoodServices.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Common;
    using NeighbourhoodServices.Web.Infrastructure;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Services.Data.Interface;

    public class CategoriesController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly ICategoriesService categoryService;

        public CategoriesController(IAnnouncementService announcementService , ICategoriesService categoryService)
        {
            this.announcementService = announcementService;
            this.categoryService = categoryService;
        }

        [Route("{categoryName}/{currentPage?}")]
        public IActionResult AnnouncementsByCategory(string categoryName, int currentPage = 1)
        {
            var skip = (currentPage - 1) * AnnouncementsConstants.AnnouncementsPerPage;
            var pageModel = new Page
            {
                CurrentPage = currentPage,
            };

            pageModel.AnnouncementsCount = this.announcementService.AllAnnouncementByCategoryCount(categoryName);

            var announcementViewModel = this.announcementService.GetByCategory<AnnouncementViewModel>(categoryName, skip);
            var categories = this.categoryService.GetAll<IndexCategoriesView>();
            var viewModel = new GetAllViewModel()
            {
                Announcements = announcementViewModel,
                Page = pageModel,
                Categories = categories,
            };
            return this.View(viewModel);

        }
    }
}
