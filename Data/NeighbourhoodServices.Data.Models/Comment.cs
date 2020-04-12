namespace NeighbourhoodServices.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using NeighbourhoodServices.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }
    }
}
