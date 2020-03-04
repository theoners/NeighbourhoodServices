// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System;
    using System.Collections.Generic;

    using NeighbourhoodServices.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Services = new HashSet<Service>();
        }

        public string Name { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
