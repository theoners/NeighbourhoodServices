using System.Collections.Generic;

namespace NeighbourhoodServices.Services.Data.Interface
{
    using System.Threading.Tasks;
    using NeighbourhoodServices.Web.ViewModels.Comments;

    public interface ICommentService
    {
        Task<int> CreateAsync(CommentInputModel commentInputModel);

        IEnumerable<T> GetCommentByPostId<T>(string announcementId, int skip = 0);

        IEnumerable<T> GetCommentByUserId<T>(string userId);

        Task<int> DeleteAsync(int id);

        Task<string> UpdateAsync(CommentViewModel commentViewModel);
    }
}
