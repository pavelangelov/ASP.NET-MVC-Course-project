using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class ImageController : Controller
    {
        private IImageGalleryService imageGalleryService;
        private IImageFactory imageFactory;
        private IDateProvider dateProvider;
        private ILakeService lakeService;

        public ImageController(IImageGalleryService imageGalleryService, IImageFactory imageFactory, IDateProvider dateProvider, ILakeService lakeService)
        {
            Validator.ValidateForNull(imageGalleryService, paramName: "imageGalleryService");
            Validator.ValidateForNull(imageFactory, paramName: "imageFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");
            Validator.ValidateForNull(lakeService, paramName: "lakeService");

            this.imageGalleryService = imageGalleryService;
            this.imageFactory = imageFactory;
            this.dateProvider = dateProvider;
            this.lakeService = lakeService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            var model = new AddImageViewModel();
            this.LoadNames(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HttpPostedFileBase file, AddImageViewModel model)
        {
            if (file == null)
            {
                ModelState.AddModelError("", "Не е избран файл!");
            }

            if (ModelState.IsValid)
            {
                var isValidaFIle = file.ContentLength > 0 && file.ContentLength <= Constants.ImageMaxSize;

                if (isValidaFIle)
                {
                    try
                    {
                        var date = this.dateProvider.GetDate();
                        var url = file.FileName; // TODO: fix this!!!
                        // TODO: Save file!
                        var image = this.imageFactory.CreateImage(url, date, model.ImageInfo);
                        if (User.IsInRole("Moderator"))
                        {
                            image.IsConfirmed = true;
                        }

                        var gallery = this.imageGalleryService.FindById(model.SelectedImageGalleryId);

                        //gallery.Images.Add(image);

                        //this.imageGalleryService.Save();

                    }
                    catch (ArgumentException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }

            this.LoadNames(model);

            return View(model);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public ActionResult Remove()
        {
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string imageId)
        {
            // TODO: Remove image from service!
            return View();
        }


        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public ActionResult Confirm()
        {
            // TODO: Get all unconfirmed images from service, and show it!
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(string imageId)
        {
            // TODO: Get image from service and confirm it!
            return View();
        }

        private void LoadNames(AddImageViewModel model)
        {
            var lakes = this.lakeService.GetAll();

            model.Lakes = lakes;
        }
    }
}