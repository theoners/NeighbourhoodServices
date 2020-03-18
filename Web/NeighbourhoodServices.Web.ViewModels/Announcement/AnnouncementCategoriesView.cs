namespace NeighbourhoodServices.Web.ViewModels.Announcement
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class AnnouncementCategoriesView : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
