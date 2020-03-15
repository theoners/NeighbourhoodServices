namespace NeighbourhoodServices.Web.ViewModels.Announcement
{
    using Data.Models;
    using Services.Mapping;

    public class AnnouncementCategoriesView : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
