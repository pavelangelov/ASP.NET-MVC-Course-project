﻿using System;
using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class VideoController : ModeratorBaseController
    {
        private IVideoService videoService;
        private IVideoFactory videoFactory;
        private IDateProvider dateProvider;

        public VideoController(IVideoService videoService, IVideoFactory videoFactory, IDateProvider dateProvider)
        {
            Validator.ValidateForNull(videoService, "videoService");
            Validator.ValidateForNull(videoFactory, "videoFactory");
            Validator.ValidateForNull(dateProvider, "dateProvider");

            this.videoService = videoService;
            this.videoFactory = videoFactory;
            this.dateProvider = dateProvider;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new AddVideoViewModel();
            this.SetGalleryNames(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddVideoViewModel model)
        {
            this.SetGalleryNames(model);
            string galleryName;

            if (model.GalleryId != null)
            {
                galleryName = this.videoService.GetGalleryNameById(model.GalleryId);
            }
            else
            {
                galleryName = model.NewGalleryName;
            }

            if (model.VideoUrl == null || model.VideoUrl.Length == 0)
            {
                return Json(new { status = "error", message = GlobalMessages.InvalidVideoUrlMessage });
            }

            if (galleryName == null || galleryName.Length == 0)
            {
                return Json(new { status = "error", message = GlobalMessages.InvalidGalleryNameMessage });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var date = this.dateProvider.GetDate();
                    var url = model.VideoUrl.Replace("watch?v=", "embed/");
                    var video = this.videoFactory.CreateVideo(model.VideoTitle, url, date);
                    this.videoService.AddVideoToGallery(galleryName, video);
                    this.videoService.Save();
                    return Json(new { status = "success", message = GlobalMessages.AddVideoSuccessMessage });
                }
                catch (Exception)
                {
                    return Json(new { status = "error", message = GlobalMessages.AddVideoErrorMessage });
                }
            }
            else
            {
                return Json(new { status = "error", message = GlobalMessages.InvalidVideoTitleMessage });
            }
        }

        [HttpGet]
        public ActionResult Remove()
        {
            var allGalleries = this.videoService.GetAll();
            var model = new RemoveVideoViewModel();
            model.Galleries = allGalleries;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Remove(string videoId, string galleryName)
        {
            bool result;
            try
            {
                result = this.videoService.RemoveVideoFromGallery(galleryName, videoId);
                this.videoService.Save();
            }
            catch (Exception)
            {
                result = false;
            }
            
            if (result)
            {
                return Json(new { status = "success", message = GlobalMessages.RemoveVideoSuccessMessage });
            }

            return Json(new { status = "error", message = GlobalMessages.RemoveVideoErroMessage });
        }

        private void SetGalleryNames(AddVideoViewModel model)
        {
            var names = this.videoService.GetAll().ToList();
            model.SetNames(names);
        }
    }
}