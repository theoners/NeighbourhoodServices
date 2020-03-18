namespace NeighbourhoodServices.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Services.Data;
    using NeighbourhoodServices.Web.ViewModels;
    using NeighbourhoodServices.Web.ViewModels.Administration.Dashboard;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ICategoriesService categoriesService, ApplicationDbContext dbContext)
        {
            this.categoriesService = categoriesService;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories =
                    this.categoriesService.GetAll<IndexCategoriesView>(),
                AspNetUsersCount = this.dbContext.Users.Count(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Announcement()
        {
            return this.View();
        }
    }
}
