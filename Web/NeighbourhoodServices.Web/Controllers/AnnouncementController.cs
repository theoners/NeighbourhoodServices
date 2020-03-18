using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAnnouncement(AnnouncementInputModel announcementInputModel)
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
