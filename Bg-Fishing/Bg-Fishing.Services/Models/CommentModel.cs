using System;
using System.Collections.Generic;
using System.Linq;

using Bg_Fishing.Models.Comments;

namespace Bg_Fishing.Services.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        
        public string Content { get; set; }
        
        public string LakeName { get; set; }

        public DateTime PostedDate { get; set; }

        public string Username { get; set; }

        public IEnumerable<InnerCommentModel> Comments { get; set; }

        public static Func<Comment, CommentModel> Cast
        {
            get
            {
                return c => new CommentModel()
                {
                    Id = c.Id,
                    Content = c.Content,
                    LakeName = c.LakeName,
                    Username = c.Username,
                    PostedDate = c.PostedDate,
                    Comments = c.Comments.Select(InnerCommentModel.Cast)
                };
            }
        }
    }
}