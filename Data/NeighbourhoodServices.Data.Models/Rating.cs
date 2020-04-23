using NeighbourhoodServices.Data.Common.Models;

namespace NeighbourhoodServices.Data.Models
{
    public class Rating : BaseDeletableModel<int>
    {
        public double TotalRating { get; set; }

        public string AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
