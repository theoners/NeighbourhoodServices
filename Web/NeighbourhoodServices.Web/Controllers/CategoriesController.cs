using Microsoft.AspNetCore.Mvc;
using NeighbourhoodServices.Services.Data;
using NeighbourhoodServices.Web.ViewModels.Categories;

namespace NeighbourhoodServices.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult GetAll()
        {
            var viewModel =
                this.categoriesService.GetAll<IndexCategoriesView>();
            return this.View(viewModel);
        }
    }

   
}
