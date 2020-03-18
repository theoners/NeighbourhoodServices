namespace NeighbourhoodServices.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NeighbourhoodServices.Web.ViewModels.Announcement;

    public interface IAnnouncementService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetByCreatedOn<T>(int? count = null);

        IEnumerable<T> GetByCategory<T>(string categoryName, int? count = null);

        Task<string> CreateAsync(AnnouncementInputModel announcementInputModel,string userId);
    }
}
