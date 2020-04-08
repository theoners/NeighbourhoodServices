namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    public class AnnouncementUpdateModel
    {
        public IEnumerable<AnnouncementCategoriesView> Categories { get; set; }

        public AnnouncementViewModel Announcement { get; set; }
    }
}
