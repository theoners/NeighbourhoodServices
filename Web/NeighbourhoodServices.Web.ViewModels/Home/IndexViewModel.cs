namespace NeighbourhoodServices.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Categories;
    using NeighbourhoodServices.Web.ViewModels.Users;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoriesView> Categories { get; set; }

        public int SettingsCount { get; set; }

        public IEnumerable<AnnouncementViewModel> Announcements { get; set; }

        public int AspNetUsersCount { get; set; }

        public IEnumerable<TopUserViewModel> TopUsers { get; set; }
    }
}
