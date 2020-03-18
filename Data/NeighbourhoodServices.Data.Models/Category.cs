// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NeighbourhoodServices.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Services = new HashSet<Announcement>();
        }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Announcement> Services { get; set; }
    }
}
