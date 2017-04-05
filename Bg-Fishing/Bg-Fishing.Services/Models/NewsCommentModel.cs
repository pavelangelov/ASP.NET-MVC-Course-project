using System;

using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Models
{
    public class NewsCommentModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime PostedOn { get; set; }

        public static Func<NewsComment, NewsCommentModel> Cast
        {
            get
            {
                return n => new NewsCommentModel()
                {
                    Username = n.Username,
                    Content = n.Content,
                    PostedOn = n.PostedOn
                };
            }
        }
    }
}
