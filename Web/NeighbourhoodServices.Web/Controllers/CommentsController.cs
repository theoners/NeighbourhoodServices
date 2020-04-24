namespace NeighbourhoodServices.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NeighbourhoodServices.Services.Data.Interface;
    using NeighbourhoodServices.Web.ViewModels.Announcements;
    using NeighbourhoodServices.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentInputModel commentInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            await this.commentService.CreateAsync(commentInputModel);

            return this.RedirectToAction("GetDetails", "Announcements", new { id = commentInputModel.AnnouncementId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int commentId)
        {
            var currentUrl = this.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Referer").Value;
            var announcementId = await this.commentService.DeleteAsync(commentId);
            return this.Redirect(currentUrl);
        }
    }
}
