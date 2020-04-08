using Microsoft.EntityFrameworkCore;

namespace NeighbourhoodServices.Services.Data.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Web.ViewModels.Announcements;

    public interface IAnnouncementService
    {
        T GetDetails<T>(string id);

        IEnumerable<T> GetByCreatedOn<T>(int skip = 0);

        IEnumerable<T> GetByCategory<T>(string categoryName, int skip = 0);

        IEnumerable<T> GetByUser<T>(string userId, int skip = 0);

        Task<string> CreateAsync(AnnouncementInputModel announcementInputModel, string userId);

        int AllAnnouncementCount();

        int AllAnnouncementByCategoryCount(string name);

        Task<int> DeleteAsync(string id);
    }
}
