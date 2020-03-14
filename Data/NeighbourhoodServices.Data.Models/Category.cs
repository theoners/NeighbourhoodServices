// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Services = new HashSet<Service>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
