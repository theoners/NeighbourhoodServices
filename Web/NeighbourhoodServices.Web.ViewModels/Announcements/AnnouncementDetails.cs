namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using System;
    using System.Collections.Generic;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Comments;

    public class AnnouncementDetails : IMapFrom<Announcement>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public string Place { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Price { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
