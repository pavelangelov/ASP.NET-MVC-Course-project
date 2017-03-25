using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.MvcClient.Models;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public const int NewsCount = 5;

        private INewsService newsService;

        public HomeController(INewsService newsService)
        {
            Validator.ValidateForNull(newsService, paramName: "newsService");

            this.newsService = newsService;
        }

        public ActionResult Index(int page = 0)
        {
            var skip = page * NewsCount;
            var news = this.newsService.GetNews(skip, NewsCount).ToList();
            var model = new HomeViewModel();
            model.News = news;

            var allNewsCount = this.newsService.GetNewsCount();
            if (allNewsCount - (skip + NewsCount) > 0)
            {
                model.HasMoreNews = true;
                model.NextPage = page + 1;
            }

            return View(model);
        }
    }
}