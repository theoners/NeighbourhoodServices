using System.Threading.Tasks;

namespace NeighbourhoodServices.Services.Data.Service
{
    using System.Linq;

    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data.Interface;

    public class RatingService : IRatingService
    {
        private readonly IDeletableEntityRepository<Rating> ratingRepository;

        public RatingService(IDeletableEntityRepository<Rating> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task<double> AddRating(string announcementId, int ratingValue, string userId)
        {
            var currentRating = this.ratingRepository.All()
                .FirstOrDefault(x => x.UserId == userId && x.AnnouncementId == announcementId);

            if (currentRating != null)
            {
                currentRating.TotalRating = ratingValue;
                await this.ratingRepository.SaveChangesAsync();
            }
            else
            {
                var rating = new Rating()
                {
                    UserId = userId,
                    TotalRating = ratingValue,
                    AnnouncementId = announcementId,
                };
                await this.ratingRepository.AddAsync(rating);
                await this.ratingRepository.SaveChangesAsync();
            }

            return this.GetCurrentRating(announcementId);
        }

        public double CurrentUserRating(string announcementId, string userId)
        {
            double currentUserRating;
            var rating = this.ratingRepository.All()
                .FirstOrDefault(x => x.UserId == userId && x.AnnouncementId == announcementId);
            if (rating != null)
            {
                currentUserRating = rating.TotalRating;
            }
            else
            {
                currentUserRating = 0;
            }

            return currentUserRating;
        }

        public double GetCurrentRating(string announcementId)
        {
            double result;
            var currentRating = this.ratingRepository.All()
                .Where(x => x.AnnouncementId == announcementId);
            if (currentRating.Count() != 0)
            {
                result = currentRating.Sum(x => x.TotalRating) / currentRating.Count();
            }
            else
            {
                result = -1;
            }
            return result;
        }
    }
}
