using System;

using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Models
{
    public class InnerCommentModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedDate { get; set; }

        public string Username { get; set; }

        public static Func<Comment, InnerCommentModel> Cast
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