namespace NeighbourhoodServices.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Web.ViewModels.Announcement;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoriesView> Categories { get; set; }

        public int SettingsCount { get; set; }

        public IEnumerable<AnnouncementViewModel> Announcement { get; set; }

        public int AspNetUsersCount { get; set; }
    }
}
