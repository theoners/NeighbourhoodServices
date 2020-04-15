namespace NeighbourhoodServices.Web.ViewModels.Comments
{
    using System;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AnnouncementId { get; set; }
    }
}
