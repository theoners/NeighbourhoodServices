using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Services.Mapping;

namespace NeighbourhoodServices.Web.ViewModels.Categories
{
    public class IndexCategoriesView : IMapFrom<Category>
    {
        public string Name { get; set; }

        public int ServicesCount { get; set; }
    }
}
