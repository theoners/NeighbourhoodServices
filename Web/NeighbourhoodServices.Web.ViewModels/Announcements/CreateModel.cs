namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    public class CreateModel
    {
        public AnnouncementInputModel Announcement { get; set; }

        public IEnumerable<AnnouncementCategoriesView> Categories { get; set; }
    }
}
