using System;
using System.Collections.Generic;
using System.Text;
using NeighbourhoodServices.Web.ViewModels.Announcement;
using NeighbourhoodServices.Web.ViewModels.Categories;

namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
   public class GetAllViewModel
    {
        public IEnumerable<AnnouncementViewModel> Announcements { get; set; }

        public IEnumerable<IndexCategoriesView> Categories { get; set; }
    }
}
