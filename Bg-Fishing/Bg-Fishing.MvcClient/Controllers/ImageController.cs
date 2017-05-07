using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;
using System;

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
            if (ModelState.IsValid && file != null)
            {
                if (file.ContentLength > 0 && file.ContentLength <= Constants.ImageMaxSize)
                {
                    try
                    {
                        var date = this.dateProvider.GetDate();
                        var url = file.FileName; // TODO: fix this!!!
                        var image = this.imageFactory.CreateImage(url, date, model.ImageInfo);

                        var gallery = this.imageGalleryService.FindById(model.SelectedImageGalleryId);

                        gallery.Images.Add(image);

                        this.imageGalleryService.Save();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string imageId)
        {
            return View();
        }

        private void LoadNames(AddImageViewModel model)
        {
            var lakeNames = this.lakeService.GetAll();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "-----" });

            foreach (var lake in lakeNames)
            {
                list.Add(new SelectListItem() { Text = lake.Name, Value = lake.Id });
            }

            model.GalleryNames = list;
        }
    }
}