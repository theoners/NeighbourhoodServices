namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Web.ViewModels.Announcement;
    using NeighbourhoodServices.Web.ViewModels.Home;

    public class GetAllViewModel
    {
        public IEnumerable<AnnouncementViewModel> Announcements { get; set; }

        public IEnumerable<IndexCategoriesView> Categories { get; set; }
    }
}
