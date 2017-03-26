﻿using System;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class NewsController : ModeratorBaseController
    {
        private INewsFactory newsFactory;
        private INewsService newsService;
        private IDateProvider dateProvider;

        public NewsController(INewsFactory newsFactory, INewsService newsService, IDateProvider dateProvider)
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
        [ValidateInput(false)]
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
                    TempData["AddNewsSuccess"] = "Новината е добавена";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                return View(model);
            }

            return View(model);
        }
    }
}