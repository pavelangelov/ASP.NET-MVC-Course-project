using System;

using Bg_Fishing.Models.Comments;

namespace Bg_Fishing.Services.Models
{
    public class InnerCommentModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedDate { get; set; }

        public string Username { get; set; }

        public static Func<InnerComment, InnerCommentModel> Cast
        {
            get
            {
                return c => new InnerCommentModel()
                {
                    Id = c.Id,
                    Username = c.Username,
                    Content = c.Content,
                    PostedDate = c.PostedDate
                };
            }
        }
    }
}