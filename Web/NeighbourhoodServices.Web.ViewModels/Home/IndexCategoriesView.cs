namespace NeighbourhoodServices.Web.ViewModels.Categories
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class IndexCategoriesView : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ServicesCount { get; set; }
    }
}
