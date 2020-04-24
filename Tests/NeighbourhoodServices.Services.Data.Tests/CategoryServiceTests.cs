namespace NeighbourhoodServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

    public class CategoryServiceTests : IDisposable
    {
        private readonly EfDeletableEntityRepository<Category> categoryRepository;
        private readonly ApplicationDbContext dbContext;

        public CategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
            this.dbContext = new ApplicationDbContext(options);
            this.categoryRepository = new EfDeletableEntityRepository<Category>(this.dbContext);
            AutoMapperConfig.RegisterMappings(typeof(AnnouncementCategoriesView).Assembly, typeof(Announcement).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllWorkCorrectly()
        {
            var categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                var category = new Category()
                {
                    Name = "Test" + i.ToString(),
                };
                categories.Add(category);
            }

            await this.dbContext.Categories.AddRangeAsync(categories);
            await this.dbContext.SaveChangesAsync();
            var service = new CategoriesService(this.categoryRepository);
            var result = service.GetAll<AnnouncementCategoriesView>();
            Assert.Equal(5, result.Count());
            var twoObj = service.GetAll<AnnouncementCategoriesView>(2);
            Assert.Equal(2, twoObj.Count());
        }

        [Fact]
        public async Task DeleteWorkCorrectly()
        {
            var categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                var category = new Category()
                {
                    Name = "Test" + i.ToString(),
                };
                categories.Add(category);
            }

            await this.dbContext.Categories.AddRangeAsync(categories);
            await this.dbContext.SaveChangesAsync();
            var service = new CategoriesService(this.categoryRepository);
            var result = service.GetAll<AnnouncementCategoriesView>();
            Assert.Equal(5, result.Count());
            service.Delete("1");
            var resultAfterDelete = service.GetAll<AnnouncementCategoriesView>();
            Assert.Equal(4, resultAfterDelete.Count());
        }

        [Fact]
        public async Task DeleteWorkCorrectlyWithNotCorectId()
        {
            var categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                var category = new Category()
                {
                    Name = "Test" + i.ToString(),
                };
                categories.Add(category);
            }

            await this.dbContext.Categories.AddRangeAsync(categories);
            await this.dbContext.SaveChangesAsync();
            var service = new CategoriesService(this.categoryRepository);
            var result = service.GetAll<AnnouncementCategoriesView>();
            Assert.Equal(5, result.Count());
            var isDelete = service.Delete("11");

            Assert.False(isDelete);
        }

        [Fact]
        public async Task UpdateWorkCorrectly()
        {
            var category = new Category()
            {
                Name = "Test",
            };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
            var service = new CategoriesService(this.categoryRepository);

            service.Update("Edit", "Edit", "1");
            var result = service.GetAll<AnnouncementCategoriesView>().FirstOrDefault();

            Assert.Equal("Edit", result.Name);
        }

        [Fact]
        public async Task AddWorkCorrectly()
        {

            var service = new CategoriesService(this.categoryRepository);

            await service.Add("Edit", "Edit");
            var result = service.GetAll<AnnouncementCategoriesView>().FirstOrDefault();

            Assert.Equal("Edit", result.Name);
        }
    }
}
