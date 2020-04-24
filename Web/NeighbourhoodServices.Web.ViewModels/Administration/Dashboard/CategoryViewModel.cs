namespace NeighbourhoodServices.Web.ViewModels.Administration.Dashboard
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ServicesCount { get; set; }
    }
}
