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
    using NeighbourhoodServices.Services.Mapping;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Comments;
    using Xunit;

    public class CommentServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly ApplicationDbContext dbContext;

        public CommentServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
            this.dbContext = new ApplicationDbContext(options);
            this.commentRepository = new EfDeletableEntityRepository<Comment>(this.dbContext);
            AutoMapperConfig.RegisterMappings(typeof(AnnouncementViewModel).Assembly, typeof(Announcement).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CreateAsyncAddCommentCorrectly()
        {
            var comment = new CommentInputModel()
            {
                Text = "test",
                UserId = "a",
            };
            var service = new CommentsService(this.commentRepository);
            await service.CreateAsync(comment);
            Assert.Equal(1, this.dbContext.Comments.Count());
        }

        [Fact]
        public async Task GetCommentByPostIdWorkCorrectly()
        {
            var comment = new Comment()
            {
                Text = "test",
                UserId = "a",
                AnnouncementId = "1",
            };
            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();
            var service = new CommentsService(this.commentRepository);
            var result = service.GetCommentByPostId<CommentInputModel>("1");
            Assert.Equal(1, result?.Count());
        }

        [Fact]
        public async Task GetCommentByUserIdWorkCorrectly()
        {
            var comment = new Comment()
            {
                Text = "test",
                UserId = "a",
                AnnouncementId = "1",
            };
            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();
            var service = new CommentsService(this.commentRepository);
            var result = service.GetCommentByUserId<CommentInputModel>("a");
            Assert.Equal(1, result?.Count());
        }

        [Fact]
        public async Task DeleteAsyncWorkCorrectly()
        {
            var comment = new Comment()
            {
                Text = "test",
                UserId = "a",
                AnnouncementId = "1",
            };
            var obj = await this.dbContext.Comments.AddAsync(comment);
            var commentId = obj.Entity.Id;
            await this.dbContext.SaveChangesAsync();
            var service = new CommentsService(this.commentRepository);
            var result = service.GetCommentByUserId<CommentInputModel>("a");
            Assert.Equal(1, result?.Count());
            await service.DeleteAsync(commentId);
            result = service.GetCommentByUserId<CommentInputModel>("a");
            Assert.Equal(0, result?.Count());
        }
    }
}
