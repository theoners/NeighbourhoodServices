namespace NeighbourhoodServices.Web.ViewModels.Users
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;
    public class TopUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public int AnnouncementsCount { get; set; }
    }
}
