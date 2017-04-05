using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Services.Models;

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

        public void Add(News news)
        {
            this.dbContext.News.Add(news);
        }

        public News FindById(string id)
        {
            var news = this.dbContext.News.Find(id);

            return news;
        }

        public IEnumerable<NewsModel> GetNews(int skip, int take)
        {
            var news = this.dbContext.News.Include(n => n.Comments)
                                            .OrderByDescending(n => n.PostedOn)
                                            .Skip(skip)
                                            .Take(take);

            if (news != null)
            {
                return news.Select(NewsModel.Cast);
            }

            return Enumerable.Empty<NewsModel>();
        }

        public int GetNewsCount()
        {
            return this.dbContext.News.Count();
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
