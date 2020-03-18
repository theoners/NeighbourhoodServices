namespace NeighbourhoodServices.Web.ViewModels
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class ServiceViewModel : IMapFrom<Service>
    {
        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public string Place { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
