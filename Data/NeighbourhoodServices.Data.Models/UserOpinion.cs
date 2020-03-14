namespace NeighbourhoodServices.Data.Models
{
     public class UserOpinion
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string OpinionId { get; set; }

        public Opinion Opinion { get; set; }
    }
}
