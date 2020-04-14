// ReSharper disable VirtualMemberCallInConstructor
namespace NeighbourhoodServices.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using NeighbourhoodServices.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Announcements = new HashSet<Announcement>();
            this.Photos = new HashSet<Photo>();
            this.UserOpinions = new HashSet<UserOpinion>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string ProfilePictureUrl { get; set; } = "/images/avatar.png";

        public string City { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<UserOpinion> UserOpinions { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
