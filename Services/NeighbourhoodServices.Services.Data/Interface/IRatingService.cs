using System.Threading.Tasks;

namespace NeighbourhoodServices.Services.Data.Interface
{
    public interface IRatingService
    {
        Task<double> AddRating(string announcementId, int rating, string userId);

        double CurrentUserRating(string announcementId, string userId);

        double GetCurrentRating(string announcementId);
    }
}
