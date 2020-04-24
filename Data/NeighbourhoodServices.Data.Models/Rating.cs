namespace NeighbourhoodServices.Data.Models
{
    using NeighbourhoodServices.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        public double TotalRating { get; set; }

        public string AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
