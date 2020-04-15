namespace NeighbourhoodServices.Web.ViewModels.Comments
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class CommentInputModel : IMapFrom<Comment>
    {
        public string Text { get; set; }

        public string UserId { get; set; }

        public string AnnouncementId { get; set; }
    }
}
