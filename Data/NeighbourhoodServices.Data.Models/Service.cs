// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System;

    using NeighbourhoodServices.Data.Common.Models;

    public class Service : BaseDeletableModel<string>
    {
        public Service()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public string Place { get; set; }

        public Category Category { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}