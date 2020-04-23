using System.Collections.Generic;

namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    public class CreateModel
    {
        public AnnouncementInputModel Announcement { get; set; }

        public IEnumerable<AnnouncementCategoriesView> Categories { get; set; }
    }
}
