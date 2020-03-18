namespace NeighbourhoodServices.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels.Announcement;

    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly ApplicationDbContext dbContext;

        public AnnouncementController(IAnnouncementService announcementService, ApplicationDbContext dbContext)
        {
            this.announcementService = announcementService;
            this.dbContext = dbContext;
        }

        public IActionResult Announcement()
        {
            var viewModel =
                this.announcementService.GetAll<AnnouncementCategoriesView>();
            return this.View(viewModel);
        }

        public IActionResult PostAnnouncement(AnnouncementInputModel announcementInputModel, ApplicationUser user)
        {
            var model = new Service()
            {
                Description = announcementInputModel.Description,
                Place = announcementInputModel.Address,
                ServiceType = announcementInputModel.ServiceType,
                CategoryId = int.Parse(announcementInputModel.Category),
            };

            this.dbContext.Services.Add(model);
            this.dbContext.SaveChanges();

            return this.Redirect("/");
        }
    }
}
