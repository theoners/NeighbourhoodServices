// ReSharper disable VirtualMemberCallInConstructor

using NeighbourhoodServices.Data.Common.Models;

namespace NeighbourhoodServices.Data.Models
{
    using System;

    public class Photo: BaseDeletableModel<string>
    {

        public Photo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}