// ReSharper disable VirtualMemberCallInConstructor

namespace NeighbourhoodServices.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NeighbourhoodServices.Data.Common.Models;

    public class Opinion : BaseDeletableModel<int>
    {
        public Opinion()
        {
            this.UserOpinions = new HashSet<UserOpinion>();
        }

        [Required]
        [MaxLength(250)]
        public string Text { get; set; }

        [Range(0, 5)]
        public short Rating { get; set; }

        public virtual ICollection<UserOpinion> UserOpinions { get; set; }
    }
}
