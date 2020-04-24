namespace NeighbourhoodServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NeighbourhoodServices.Data;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Data.Repositories;
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Users;
    using Xunit;

    public class UserServiceTests : IDisposable
    {
        private readonly EfDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext dbContext;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
            this.dbContext = new ApplicationDbContext(options);
            this.userRepository = new EfDeletableEntityRepository<ApplicationUser>(this.dbContext);
            AutoMapperConfig.RegisterMappings(typeof(UserViewModel).Assembly, typeof(Announcement).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetUserCountWorkCorrectly()
        {
            var users = new List<ApplicationUser>();
            for (int i = 0; i < 5; i++)
            {
                var user = new ApplicationUser()
                {
                    Email = "test@test" + i.ToString(),
                    UserName = "Test" + i.ToString(),
                };
                users.Add(user);
            }

            await this.dbContext.Users.AddRangeAsync(users);
            await this.dbContext.SaveChangesAsync();
            var service = new UsersService(this.userRepository);
            var userCount = service.GetUserCount();
            Assert.Equal(5, userCount);
        }

        [Fact]
        public async Task GetUserWorkCorrectly()
        {
            var user = new ApplicationUser()
            {
                Email = "test@test",
                UserName = "Test",
            };

            var result = await this.dbContext.Users.AddAsync(user);
            var userId = result.Entity.Id;
            await this.dbContext.SaveChangesAsync();
            var service = new UsersService(this.userRepository);
            var ExpectedUser = service.GetUser<UserViewModel>(userId);
            Assert.Equal(user.UserName, ExpectedUser.UserName);
        }

        [Fact]
        public async Task SearchUserWorkCorrectly()
        {
            var users = new List<ApplicationUser>();
            for (int i = 0; i < 5; i++)
            {
                var user = new ApplicationUser()
                {
                    Email = "test@test" + i.ToString(),
                    UserName = "Test" + i.ToString(),
                    City = "city" + i.ToString(),
                };
                users.Add(user);
            }

            await this.dbContext.Users.AddRangeAsync(users);
            await this.dbContext.SaveChangesAsync();
            var service = new UsersService(this.userRepository);
            var expectedUser = service.SearchUser<UserViewModel>("test", "city");
            Assert.Equal("Test0", expectedUser.UserName);
        }

        [Fact]
        public async Task GetTopUsersWorkCorrectly()
        {
            var users = new List<ApplicationUser>();
            for (int i = 0; i < 5; i++)
            {
                var user = new ApplicationUser()
                {
                    Email = "test@test" + i.ToString(),
                    UserName = "Test" + i.ToString(),
                    City = "city" + i.ToString(),
                };
                for (int j = 0; j < i; j++)
                {
                    user.Announcements.Add(new Announcement()
                    {
                        Title = j.ToString(),
                    });
                }

                users.Add(user);
            }

            await this.dbContext.Users.AddRangeAsync(users);
            await this.dbContext.SaveChangesAsync();
            var service = new UsersService(this.userRepository);
            var expectedUser = service.GetTopUsers<UserViewModel>().FirstOrDefault();
            Assert.Equal("Test4", expectedUser?.UserName);
        }
    }
}
