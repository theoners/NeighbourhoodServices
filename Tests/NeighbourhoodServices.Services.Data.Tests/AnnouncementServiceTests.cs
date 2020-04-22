namespace NeighbourhoodServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Data.Repositories;
    using NeighbourhoodServices.Services.Data.Service;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using Xunit;

    public class AnnouncementServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Announcement> announcementRepository;
        private readonly EfDeletableEntityRepository<Category> categoryRepository;
        private readonly ApplicationDbContext dbContext;

        public AnnouncementServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
            this.dbContext = new ApplicationDbContext(options);
            this.announcementRepository = new EfDeletableEntityRepository<Announcement>(this.dbContext);
            this.categoryRepository = new EfDeletableEntityRepository<Category>(this.dbContext);
            AutoMapperConfig.RegisterMappings(typeof(AnnouncementViewModel).Assembly, typeof(Announcement).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CreateAsyncAddAnnouncementCorrectly()
        {
            var announcement = new AnnouncementInputModel()
            {
                ServiceType = ServiceType.Предмет,
                Category = "1",
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
            };
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            await service.CreateAsync(announcement, "aa");
            Assert.Equal(1, this.dbContext.Announcements.Count());
        }

        [Fact]
        public async Task CreateAsyncAddAnnouncementReturnId()
        {
            var announcement = new AnnouncementInputModel()
            {
                ServiceType = ServiceType.Предмет,
                Category = "1",
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
            };
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedId = await service.CreateAsync(announcement, "aa");
            var actualId = this.dbContext.Announcements.FirstOrDefault()?.Id;
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async Task GetByCategoryReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };
            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
            };

            await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedAnnouncements = service.GetByCategory<AnnouncementInputModel>("Aa");
            Assert.Single(expectedAnnouncements);
        }

        [Fact]
        public async Task GetByCreatedOnReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcements = new List<Announcement>();
            for (int i = 0; i < 5; i++)
            {
                var announcement = new Announcement()
                {
                    ServiceType = ServiceType.Предмет,
                    Category = category,
                    Description = i.ToString(),
                    Place = i.ToString(),
                    Title = i.ToString(),
                    UserId = i.ToString(),
                };
                announcements.Add(announcement);
            }

            await this.dbContext.Announcements.AddRangeAsync(announcements);
            await this.dbContext.SaveChangesAsync();
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedAnnouncements = service.GetByCreatedOn<AnnouncementInputModel>();
            Assert.Equal(5, expectedAnnouncements.Count());
        }

        [Fact]
        public async Task GetDetailsReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            var obj = await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();
            var id = obj.Entity.Id;
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedAnnouncements = service.GetDetails<AnnouncementInputModel>(id);
            Assert.Equal(11, expectedAnnouncements.Price);
        }

        [Fact]
        public async Task GetByUserReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedAnnouncements = service.GetByUser<AnnouncementInputModel>("a").FirstOrDefault();
            Assert.Equal(11, expectedAnnouncements?.Price);
        }

        [Fact]
        public async Task AllAnnouncementByCategoryCountReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcements = new List<Announcement>();
            for (int i = 0; i < 5; i++)
            {
                var announcement = new Announcement()
                {
                    ServiceType = ServiceType.Предмет,
                    Category = category,
                    Description = i.ToString(),
                    Place = i.ToString(),
                    Title = i.ToString(),
                    UserId = i.ToString(),
                };
                announcements.Add(announcement);
            }

            await this.dbContext.Announcements.AddRangeAsync(announcements);
            await this.dbContext.SaveChangesAsync();

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedAnnouncementsCount = service.AllAnnouncementByCategoryCount(category.Name);
            Assert.Equal(5, expectedAnnouncementsCount);
        }

        [Fact]
        public async Task UpdateAsyncReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            var obj = await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();
            var id = obj.Entity.Id;

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var announcementForUpdate = service.GetDetails<AnnouncementInputModel>(id);
            announcementForUpdate.Place = "Varna";
            announcementForUpdate.Category = "1";
            await service.UpdateAsync(announcementForUpdate, id);
            Assert.Equal("Varna", announcementForUpdate.Place);
        }

        [Fact]
        public async Task GetByKeyWordSearchCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var result = service.GetByKeyWord<AnnouncementInputModel>("test", null, null).FirstOrDefault();
            Assert.Equal("test", result?.Place);
        }

        [Fact]
        public async Task GetByKeyWordCityCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var result = service.GetByKeyWord<AnnouncementInputModel>(null, null, "test").FirstOrDefault();
            Assert.Equal("test", result?.Place);
        }

        [Fact]
        public async Task GetByKeyWordCategoryCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var result = service.GetByKeyWord<AnnouncementInputModel>("test", category.Name, null).FirstOrDefault();
            Assert.Equal("test", result?.Place);
        }

        [Fact]
        public async Task GetByKeyWordThreeArgsCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcement = new Announcement()
            {
                ServiceType = ServiceType.Предмет,
                Category = category,
                Description = "test",
                Place = "test",
                Title = "test",
                UserId = "a",
                Price = 11,
            };

            await this.dbContext.Announcements.AddAsync(announcement);
            await this.dbContext.SaveChangesAsync();

            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var result = service.GetByKeyWord<AnnouncementInputModel>("test", category.Name, "test").FirstOrDefault();
            Assert.Equal(11, result?.Price);
        }

        [Fact]
        public async Task AllAnnouncementReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcements = new List<Announcement>();
            for (int i = 0; i < 5; i++)
            {
                var announcement = new Announcement()
                {
                    ServiceType = ServiceType.Предмет,
                    Category = category,
                    Description = i.ToString(),
                    Place = i.ToString(),
                    Title = i.ToString(),
                    UserId = i.ToString(),
                };
                announcements.Add(announcement);
            }

            await this.dbContext.Announcements.AddRangeAsync(announcements);
            await this.dbContext.SaveChangesAsync();
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var expectedAnnouncementsCount = service.AllAnnouncementCount();
            Assert.Equal(5, expectedAnnouncementsCount);
        }

        [Fact]
        public async Task DeleteAsyncReturnCorrectResult()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Aa",
            };

            var announcements = new List<Announcement>();
            for (int i = 0; i < 5; i++)
            {
                var announcement = new Announcement()
                {
                    ServiceType = ServiceType.Предмет,
                    Category = category,
                    Description = i.ToString(),
                    Place = i.ToString(),
                    Title = i.ToString(),
                    UserId = i.ToString(),
                };
                announcements.Add(announcement);
            }

            await this.dbContext.Announcements.AddRangeAsync(announcements);
            await this.dbContext.SaveChangesAsync();
            var service = new AnnouncementService(this.categoryRepository, this.announcementRepository);
            var id = this.dbContext.Announcements.FirstOrDefault()?.Id;
            var expectedAnnouncementsCount = service.DeleteAsync(id);
            var announcementCount = service.AllAnnouncementCount();
            Assert.Equal(4, announcementCount);
        }
    }
}
