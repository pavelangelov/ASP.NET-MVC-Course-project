﻿using Bg_Fishing.Models.Galleries;
using Bg_Fishing.MvcClient.Models.ViewModels.Moderator;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Controllers.Moderator
{
    [Authorize(Roles = "Moderator")]
    public class ManageVideosController : Controller
    {
        private IVideoService videoService;

        public ManageVideosController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, "videoService");

            this.videoService = videoService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new AddVideoViewModel();
            this.SetGalleryNames(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(AddVideoViewModel model)
        {
            this.SetGalleryNames(model);
            var galleryName = model.GalleryName == null ? model.NewGalleryName : model.GalleryName;
            if (model.VideoUrl == null || model.VideoUrl.Length == 0)
            {
                return Json(new { status = "error", message = "Линкът към видеото е невалиден." });
            }

            if (galleryName == null || galleryName.Length == 0)
            {
                return Json(new { status = "error", message = "Не е избрана категория." });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Add video to Gallery.
                    var video = new Video(model.VideoTitle, model.VideoUrl, DateTime.Now);
                    this.videoService.AddVideoToGallery(galleryName, video);
                    this.videoService.Save();
                    return Json(new { status = "success", message = "Видеото е добавено." });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", message = "Видеото не може да бъде добавено." });
                }
            }
            else
            {
                return Json(new { status = "error", message = "Невалидно загалвие на видеото." });
            }
        }

        private void SetGalleryNames(AddVideoViewModel model)
        {
            var names = this.videoService.GetAll().ToList();
            model.SetNames(names);
        }
    }
}