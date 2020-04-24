namespace NeighbourhoodServices.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Data.Repositories;
    using NeighbourhoodServices.Services.Data.Service;
    using Xunit;

    public class RatingServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Rating> ratingRepository;

        private readonly ApplicationDbContext dbContext;

        public RatingServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
            this.dbContext = new ApplicationDbContext(options);
            this.ratingRepository = new EfDeletableEntityRepository<Rating>(this.dbContext);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddRatingWorkCorrectly()
        {
            var rating = new Rating()
            {
                TotalRating = 4,
                UserId = "1",
                AnnouncementId = "1",
            };
            var service = new RatingService(this.ratingRepository);
            var result = await service.AddRating(rating.AnnouncementId, (int)rating.TotalRating, rating.UserId);
            Assert.Equal(4, result);
        }

        [Fact]
        public async Task AddRatingUpdateWorkCorrectly()
        {
            var rating = new Rating()
            {
                TotalRating = 4,
                UserId = "1",
                AnnouncementId = "1",
            };
            var service = new RatingService(this.ratingRepository);
            await service.AddRating(rating.AnnouncementId, (int)rating.TotalRating, rating.UserId);
            await service.AddRating(rating.AnnouncementId, 3, rating.UserId);
            var result = service.GetCurrentRating("1");
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task CurrentUserRatingWorkCorrectly()
        {
            var rating = new Rating()
            {
                TotalRating = 4,
                UserId = "1",
                AnnouncementId = "1",
            };
            var service = new RatingService(this.ratingRepository);
            await service.AddRating(rating.AnnouncementId, (int)rating.TotalRating, rating.UserId);
            var result = service.CurrentUserRating(rating.AnnouncementId, rating.UserId);
            Assert.Equal(4, result);
        }

        [Fact]
        public async Task CurrentUserRatingWorkCorrectlyWithNotCorrectlyId()
        {
            var rating = new Rating()
            {
                TotalRating = 4,
                UserId = "1",
                AnnouncementId = "1",
            };
            var service = new RatingService(this.ratingRepository);
            await service.AddRating(rating.AnnouncementId, (int)rating.TotalRating, rating.UserId);
            var result = service.CurrentUserRating("2", rating.UserId);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetCurrentRatingWorkCorrectly()
        {
            var rating = new Rating()
            {
                TotalRating = 4,
                UserId = "1",
                AnnouncementId = "1",
            };
            var service = new RatingService(this.ratingRepository);
            await service.AddRating(rating.AnnouncementId, (int)rating.TotalRating, rating.UserId);
            service.GetCurrentRating(rating.AnnouncementId);
            Assert.Equal(4, this.dbContext.Ratings.FirstOrDefault().TotalRating);
        }

        [Fact]
        public async Task GetCurrentRatingWorkCorrectlyWithNotCorrectlyId()
        {
            var rating = new Rating()
            {
                TotalRating = 4,
                UserId = "1",
                AnnouncementId = "1",
            };
            var service = new RatingService(this.ratingRepository);
            await service.AddRating(rating.AnnouncementId, (int)rating.TotalRating, rating.UserId);
            var result = service.GetCurrentRating("2");
            Assert.Equal(result, -1);
        }
    }
}
