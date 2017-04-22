using System.Linq;

using Microsoft.AspNet.SignalR;

using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Hubs
{
    public class NewsHub : Hub
    {
        private INewsService newsService;

        public NewsHub(INewsService newsService)
        {
            Validator.ValidateForNull(newsService, paramName: "newsService");

            this.newsService = newsService;
        }

        public void GetNews(int page)
        {
            var skip = page * Constants.ShowedNewsCount;
            var news = this.newsService.GetNews(skip, Constants.ShowedNewsCount).ToList();
            var hasMore = false;
            var nextPage = 0;

            var allNewsCount = this.newsService.GetNewsCount();
            if (allNewsCount - (skip + Constants.ShowedNewsCount) > 0)
            {
                hasMore = true;
                nextPage = page + 1;
            }

            Clients.Caller.loadNews(news, hasMore, nextPage );
        }
    }
}