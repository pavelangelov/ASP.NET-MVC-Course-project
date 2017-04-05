using System;
using System.Collections.Generic;
using System.Linq;

using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Models
{
    public class NewsModel
    {
        public string Id { get; set; }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PostedOn { get; set; }

        public IEnumerable<NewsCommentModel> Comments { get; set; }

        public static Func<News, NewsModel> Cast
        {
            get
            {
                return n => new NewsModel()
                {
                    Id = n.Id,
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    PostedOn = n.PostedOn,
                    Content = n.Content,
                    Comments = n.Comments.Select(NewsCommentModel.Cast)
                };
            }
        }
    }
}
