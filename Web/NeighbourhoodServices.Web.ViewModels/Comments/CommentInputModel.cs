using System;
using System.Collections.Generic;
using System.Text;
using NeighbourhoodServices.Data.Models;
using NeighbourhoodServices.Services.Mapping;

namespace NeighbourhoodServices.Web.ViewModels.Comments
{
     public class CommentInputModel : IMapFrom<Comment>
    {
        public string Text { get; set; }

        public string UserId { get; set; }

        public string AnnouncementId { get; set; }
    }
}
