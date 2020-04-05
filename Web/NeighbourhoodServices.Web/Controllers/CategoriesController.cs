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

        public CategoriesController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
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

            var viewModel = new GetAllViewModel()
            {
                Announcements = announcementViewModel,
                Page = pageModel,

            };
            return this.View(viewModel);

        }
    }
}
