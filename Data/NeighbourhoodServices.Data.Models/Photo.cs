// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System;

    using NeighbourhoodServices.Data.Common.Models;

    public class Photo : BaseDeletableModel<int>
    {
        public Photo()
        {
        }

        public string Url { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
