using Microsoft.AspNetCore.Mvc;

namespace NeighbourhoodServices.Web.Controllers
{
    public class AnnouncementController : BaseController
    {
        public IActionResult Announcement()
        {
            return this.View();
        }
    }
}
