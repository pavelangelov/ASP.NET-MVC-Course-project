using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private INewsService newsService;
        private INewsCommentFactory newsCommentFactory;
        private IDateProvider dateProvider;

        public HomeController(
            INewsService newsService,
            INewsCommentFactory newsCommentFactory,
            IDateProvider dateProvider)
        {
            Validator.ValidateForNull(newsService, paramName: "newsService");
            Validator.ValidateForNull(newsCommentFactory, paramName: "newsCommentFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");

            this.newsService = newsService;
            this.newsCommentFactory = newsCommentFactory;
            this.dateProvider = dateProvider;
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

        [HttpGet]
        public ActionResult News(string newsId)
        {
            var news = this.newsService.GetNewsById(newsId);

            news.Comments = news.Comments.ToList().OrderByDescending(c => c.PostedOn);

            var model = new NewsDetailsViewModel() { News = news };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewsComment(NewsDetailsViewModel model)
        {
            var news = this.newsService.FindById(model.NewsId);
            if (ModelState.IsValid)
            {
                try
                {
                    var username = User.Identity.Name;
                    var date = this.dateProvider.GetDate();
                    var comment = this.newsCommentFactory.CreateNewsComment(model.Content, username, date);

                    news.Comments.Add(comment);

                    this.newsService.Save();

                    TempData["AddCommentSuccess"] = "Коментара е добавен.";

                    return RedirectToAction("News", new { newsId = news.Id });
                }
                catch (System.Exception)
                {
                    ModelState.AddModelError("", "Визникна грешка при добавянето на коментара!");
                    TempData.Remove("AddCommentSuccess");
                }
            }
            else
            {
                TempData.Remove("AddCommentSuccess");
            }
            
            model.News = this.newsService.GetNewsById(news.Id);
            return View("News", model);
        }
    }
}