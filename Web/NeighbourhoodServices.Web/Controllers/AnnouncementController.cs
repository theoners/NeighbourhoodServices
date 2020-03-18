using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
        private readonly UserManager<ApplicationUser> userManager;

        public AnnouncementController(IAnnouncementService announcementService, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.announcementService = announcementService;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public IActionResult Announcement()
        {
            var viewModel =
                this.announcementService.GetAll<AnnouncementCategoriesView>();
            return this.View(viewModel);
        }

        public async Task<IActionResult> PostAnnouncement(AnnouncementInputModel announcementInputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var model = new Service()
            {
                Description = announcementInputModel.Description,
                Place = announcementInputModel.Address,
                ServiceType = announcementInputModel.ServiceType,
                CategoryId = int.Parse(announcementInputModel.Category),
                UserId = user.Id,
            };

            this.dbContext.Services.Add(model);
            this.dbContext.SaveChanges();

            return this.Redirect("/");
        }
    }
}
