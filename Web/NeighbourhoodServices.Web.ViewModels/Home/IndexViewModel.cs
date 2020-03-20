using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Web.ViewModels.Announcement;

namespace NeighbourhoodServices.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoriesView> Categories { get; set; }

        public string CategoryName { get; set; }
        public Category Category { get; set; }

        public int SettingsCount { get; set; }

        public IEnumerable<AnnouncementViewModel> Announcement { get; set; }
        
        public int AspNetUsersCount { get; set; }

        public int ServicesCount { get; set; }
    }
}
