using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class CommentsApiController : ApiController
    {
        public const int ShowedComments = 10;

        private ICommentService commentService;
        private IInnerCommentFactory innerCommentFactory;
        private IDateProvider dateProvider;

        public CommentsApiController(
            ICommentService commentService, 
            IInnerCommentFactory innerCommentFactory,
            IDateProvider dateProvider)
        {
            Validator.ValidateForNull(commentService, paramName: "commentService");
            Validator.ValidateForNull(innerCommentFactory, paramName: "innerCommentFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");

            this.commentService = commentService;
            this.innerCommentFactory = innerCommentFactory;
            this.dateProvider = dateProvider;
        }

        [HttpGet]
        public IEnumerable<CommentModel> Comments(string name, int page)
        {
            var comments = this.commentService.GetCommentsByLakeName(name, page * ShowedComments, ShowedComments).OrderByDescending(c => c.PostedDate);

            return comments;
        }

        [HttpPost]
        [Authorize]
        public string Add(string commentId, string content)
        {
            var username = User.Identity.Name;
            var date = this.dateProvider.GetDate();
            try
            {
                var innerComment = this.innerCommentFactory.CreateInnerComment(content, username, date);
            
                var comment = this.commentService.FindById(commentId);

                comment.Comments.Add(innerComment);
                this.commentService.Save();
            }
            catch (Exception)
            {
                return "error: Коментарът не може да бъде добавен!";
            }

            return "success";
        }
    }
}
