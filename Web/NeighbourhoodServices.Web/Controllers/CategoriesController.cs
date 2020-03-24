namespace NeighbourhoodServices.Web.Controllers
{
    using NeighbourhoodServices.Services.Data.Interface;

    public class CategoriesController : BaseController
    {
        private readonly IAnnouncementService announcementService;

        public CategoriesController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }
    }
}
