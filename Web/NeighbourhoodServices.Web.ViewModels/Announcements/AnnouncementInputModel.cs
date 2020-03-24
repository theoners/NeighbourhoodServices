namespace NeighbourhoodServices.Web.ViewModels.Announcements
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class AnnouncementInputModel : IMapFrom<Data.Models.Announcement>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string PriceFor { get; set; }

        public string Address { get; set; }

        public string Category{ get; set; }

        public ServiceType ServiceType { get; set; }

        public string UserId { get; set; }
    }
}
