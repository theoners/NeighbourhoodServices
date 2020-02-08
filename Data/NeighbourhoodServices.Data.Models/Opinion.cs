// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System;

    using NeighbourhoodServices.Data.Common.Models;

    public class Opinion:BaseDeletableModel<string>
    {
        public Opinion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public short Rating { get; set; }
    }
}