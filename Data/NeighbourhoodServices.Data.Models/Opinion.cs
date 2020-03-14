// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System.Collections.Generic;

    using NeighbourhoodServices.Data.Common.Models;

    public class Opinion : BaseDeletableModel<int>
    {
        public Opinion()
        {
            this.UserOpinions = new HashSet<UserOpinion>();
        }

        public string Text { get; set; }

        public short Rating { get; set; }

        public virtual ICollection<UserOpinion> UserOpinions { get; set; }
    }
}
