using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private INewsService newsService;

        public HomeController(INewsService newsService)
        {
            Validator.ValidateForNull(newsService, paramName: "newsService");

            this.newsService = newsService;
        }

        public ActionResult Index()
        {
            var page = 0;
            var skip = page * Constants.ShowedNewsCount;
            var news = this.newsService.GetNews(skip, Constants.ShowedNewsCount).ToList();
            var model = new HomeViewModel();
            model.News = news;

            var allNewsCount = this.newsService.GetNewsCount();
            if (allNewsCount - (skip + Constants.ShowedNewsCount) > 0)
            {
                model.HasMoreNews = true;
                model.NextPage = page + 1;
            }

            return View(model);
        }
    }
}