﻿namespace NeighbourhoodServices.Web.ViewModels.Users
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Comments;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string City { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<AnnouncementViewModel> Announcements { get; set; }

        public virtual ICollection<CommentViewModel> Comments { get; set; }

        public virtual ICollection<UserOpinion> UserOpinions { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
