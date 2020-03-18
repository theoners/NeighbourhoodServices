namespace NeighbourhoodServices.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Web.ViewModels.Categories;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoriesView> Categories { get; set; }

        public int SettingsCount { get; set; }

        public int AspNetUsersCount { get; set; }

        public int ServicesCount { get; set; }
    }
}
