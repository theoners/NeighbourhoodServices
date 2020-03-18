namespace NeighbourhoodServices.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Services.Data;

    public class CategoriesController : Controller
    {
        private readonly IServicesService servicesService;

        public CategoriesController(IServicesService servicesService)
        {
            this.servicesService = servicesService;
        }
    }
}
