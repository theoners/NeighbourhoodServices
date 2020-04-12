using System.Linq;
using NeighbourhoodServices.Services.Mapping;

namespace NeighbourhoodServices.Services.Data.Service
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using NeighbourhoodServices.Data.Common.Repositories;
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Web.ViewModels.Comments;

   public class CommentsService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<int> CreateAsync(CommentInputModel commentInputModel)
        {
            var comment = new Comment()
            {
                Text = commentInputModel.Text,
                AnnouncementId = commentInputModel.AnnouncementId,
                UserId = commentInputModel.UserId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
            return comment.Id;
        }

        public IEnumerable<T> GetCommentByPostId<T>(string announcementId, int skip = 0)
        {
            var comments =
                this.commentRepository
                    .All().OrderByDescending(x => x.CreatedOn)
                    .Where(x => x.AnnouncementId == announcementId);

            return comments.To<T>().ToList();
        }

        public IEnumerable<T> GetCommentByUserId<T>(string userId)
        {
            var comments =
                this.commentRepository
                    .All()
                    .OrderByDescending(x => x.CreatedOn)
                    .Where(x => x.UserId == userId);

            return comments.To<T>().ToList();
        }
    }
}
