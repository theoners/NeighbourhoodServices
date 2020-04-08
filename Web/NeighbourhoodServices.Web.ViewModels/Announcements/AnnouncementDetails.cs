namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Data.Models;

    public class AnnouncementDetails : IMapFrom<Announcement>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public string Place { get; set; }

        public string CategoryName { get; set; }

        public string CreatedOn { get; set; }

        public Category Category { get; set; }

        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
