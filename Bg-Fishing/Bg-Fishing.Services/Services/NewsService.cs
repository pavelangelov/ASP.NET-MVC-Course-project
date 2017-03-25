using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.DTOs;
using Bg_Fishing.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Services
{
    public class NewsService : INewsService
    {
        private IDatabaseContext dbContext;

        public NewsService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, paramName: "dbContext");

            this.dbContext = dbContext;
        }

        public News FindById(string id)
        {
            var news = this.dbContext.News.Find(id);

            return news;
        }

        public IEnumerable<NewsDTO> GetNews(int skip, int take)
        {
            var news = this.dbContext.News.Include(n => n.Comments)
                                            .Skip(skip)
                                            .Take(take);

            if (news != null)
            {
                return news.Select(n => new NewsDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    ImageUrl = n.ImageUrl,
                    PostedOn = n.PostedOn,
                    Comments = n.Comments.Select(c => new NewsCommentDTO
                    {
                        Username = c.Username,
                        Content = c.Content,
                        PostedOn = c.PostedOn
                    })
                });
            }

            return Enumerable.Empty<NewsDTO>();
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
