namespace NeighbourhoodServices.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly IServicesService servicesService;

        public CategoriesController(IServicesService servicesService)
        {
            this.servicesService = servicesService;
        }

        public IActionResult GetAllServiceInCategory(string name)
        {
            var viewModel = this.servicesService.GetAll<ServiceViewModel>(name);
            return this.View(viewModel);
        }
    }
}
