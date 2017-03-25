using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class CommentController : ModeratorBaseController
    {
        private INewsFactory newsFactory;
        private INewsService newsService;
        private IDateProvider dateProvider;

        public CommentController(INewsFactory newsFactory, INewsService newsService, IDateProvider dateProvider)
        {
            Validator.ValidateForNull(newsFactory, paramName: "newsFactory");
            Validator.ValidateForNull(newsService, paramName: "newsService");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");

            this.newsFactory = newsFactory;
            this.newsService = newsService;
            this.dateProvider = dateProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AddNewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var date = this.dateProvider.GetDate();
                    var news = this.newsFactory.CreateNews(model.Title, model.Content, model.ImageUrl, date);

                    this.newsService.Add(news);
                    this.newsService.Save();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                TempData["AddNewsSuccess"] = "Новината е добавена";
                return View(model);
            }

            return View(model);
        }
    }
}