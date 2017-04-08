using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class CommentsController : ApiController
    {
        private ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            Validator.ValidateForNull(commentService, paramName: "commentService");

            this.commentService = commentService;
        }

        [HttpGet]
        public IEnumerable<CommentModel> Comments(string name, int page)
        {
            var comments = this.commentService.GetAllByLakeName(name).OrderByDescending(c => c.PostedDate);

            return comments;
        }
    }
}
