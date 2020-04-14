using System;

namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.Infrastructure;

    public class AnnouncementViewModel : IMapFrom<Announcement>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public string Place { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Price { get; set; }

        public string UserId { get; set; }

        public virtual string User { get; set; }

        public string CategoryName { get; set; }

        public Page Page { get; set; }
    }
}
