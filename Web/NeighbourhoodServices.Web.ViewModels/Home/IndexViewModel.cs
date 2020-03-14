using NeighbourhoodServices.Web.ViewModels.Categories;
using System.Collections.Generic;

namespace NeighbourhoodServices.Web.ViewModels.Administration.Dashboard
{
    public class IndexViewModel
    {
        public IEnumerable<IndexCategoriesView> Categories { get; set; }
        public int SettingsCount { get; set; }
    }
}
