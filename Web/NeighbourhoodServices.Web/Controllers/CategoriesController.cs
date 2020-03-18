using NeighbourhoodServices.Data.Models;

namespace NeighbourhoodServices.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IServicesService servicesService;
        

        public CategoriesController(ICategoriesService categoriesService , IServicesService servicesService)
        {
            this.categoriesService = categoriesService;
            this.servicesService = servicesService;
            
        }

        
        public IActionResult GetByName(string name)
        {

            var viewModel = this.servicesService.GetAll<ServiceViewModel>(name);
            return this.View(viewModel);
        }

        public IActionResult GetAll()
        {
            var viewModel =
                this.categoriesService.GetAll<IndexCategoriesView>();
            return this.View(viewModel);
        }
    }
}
