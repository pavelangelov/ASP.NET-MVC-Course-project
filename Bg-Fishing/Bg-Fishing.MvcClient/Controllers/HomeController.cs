using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

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

            return View(news);
        }
    }
}