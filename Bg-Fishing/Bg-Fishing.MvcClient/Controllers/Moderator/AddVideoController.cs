using System;
using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Models.ViewModels.Moderator;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.Controllers.Moderator
{
    [Authorize(Roles = "Moderator")]
    public class AddVideoController : Controller
    {
        private IVideoService videoService;
        private IVideoFactory videoFactory;
        private IDateProvider dateProvider;

        public AddVideoController(IVideoService videoService, IVideoFactory videoFactory, IDateProvider dateProvider)
        {
            Validator.ValidateForNull(videoService, "videoService");
            Validator.ValidateForNull(videoFactory, "videoFactory");
            Validator.ValidateForNull(dateProvider, "dateProvider");

            this.videoService = videoService;
            this.videoFactory = videoFactory;
            this.dateProvider = dateProvider;
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
                    var date = this.dateProvider.GetDate();
                    var video = this.videoFactory.CreateVideo(model.VideoTitle, model.VideoUrl, date);
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