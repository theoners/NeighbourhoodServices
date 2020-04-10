using NeighbourhoodServices.Web.ViewModels.Announcements;

namespace NeighbourhoodServices.Web.ViewModels.Users
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<AnnouncementViewModel> Services { get; set; }

        public virtual ICollection<UserOpinion> UserOpinions { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
