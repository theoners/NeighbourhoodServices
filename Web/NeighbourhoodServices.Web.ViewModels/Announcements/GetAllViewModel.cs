namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Web.Infrastructure;
    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class GetAllViewModel
    {
        public IEnumerable<AnnouncementViewModel> Announcements { get; set; }

        public IEnumerable<IndexCategoriesView> Categories { get; set; }

        public Page Page { get; set; }
    }
}
