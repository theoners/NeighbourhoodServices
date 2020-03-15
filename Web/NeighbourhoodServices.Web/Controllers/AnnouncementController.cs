namespace NeighbourhoodServices.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels.Announcement;

    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService announcementService;

        public AnnouncementController(IAnnouncementService announcementService )
        {
            this.announcementService = announcementService;
        }

        public IActionResult Announcement()
        {
            var viewModel =
                this.announcementService.GetAll<AnnouncementCategoriesView>();
            return this.View(viewModel);
        }

    }
}
