// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NeighbourhoodServices.Data.Common.Models;

    public class Announcement : BaseDeletableModel<string>
    {
        public Announcement()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
            this.Rating = new HashSet<Rating>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public ServiceType ServiceType { get; set; }

        [Required]
        [MaxLength(20)]
        public string Place { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int Price { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        [Required]
        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public IEnumerable<Rating> Rating { get; set; }
    }
}
