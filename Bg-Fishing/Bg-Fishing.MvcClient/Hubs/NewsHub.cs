using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Hubs
{
    public class NewsHub : Hub
    {
        public const int NewsCount = 5;

        private INewsService newsService;

        public NewsHub(INewsService newsService)
        {
            Validator.ValidateForNull(newsService, paramName: "newsService");

            this.newsService = newsService;
        }

        public void GetNews(int page)
        {
            var skip = page * NewsCount;
            var news = this.newsService.GetNews(skip, NewsCount).ToList();
            var hasMore = false;
            var nextPage = 0;

            var allNewsCount = this.newsService.GetNewsCount();
            if (allNewsCount - (skip + NewsCount) > 0)
            {
                hasMore = true;
                nextPage = page + 1;
            }

            Clients.All.loadNews(news, hasMore, nextPage );
        }
    }
}