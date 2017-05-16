using System;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

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
        private ILakeService lakeService;
        private IImageFactory imageFactory;
        private IImageGalleryFactory imageGalleryFactory;
        private IDateProvider dateProvider;
        private IDirectoryHelper directoryHelper;

        public ImageController(
            IImageGalleryService imageGalleryService, 
            IImageFactory imageFactory, 
            IDateProvider dateProvider, 
            ILakeService lakeService,
            IImageGalleryFactory imageGalleryFactory,
            IDirectoryHelper directoryHelper)
        {
            Validator.ValidateForNull(imageGalleryService, paramName: "imageGalleryService");
            Validator.ValidateForNull(imageFactory, paramName: "imageFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");
            Validator.ValidateForNull(lakeService, paramName: "lakeService");
            Validator.ValidateForNull(imageGalleryFactory, paramName: "imageGalleryFactory");
            Validator.ValidateForNull(directoryHelper, paramName: "directoryHelper");

            this.imageGalleryService = imageGalleryService;
            this.imageFactory = imageFactory;
            this.dateProvider = dateProvider;
            this.lakeService = lakeService;
            this.imageGalleryFactory = imageGalleryFactory;
            this.directoryHelper = directoryHelper;
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
                ModelState.AddModelError("", GlobalMessages.NoFileErrorMessage);
            }
            else if (ModelState.IsValid)
            {
                var isValidFIle = file.ContentLength > 0 && file.ContentLength <= Constants.ImageMaxSize;

                if (isValidFIle)
                {
                    try
                    {
                        var date = this.dateProvider.GetDate();
                        var lakeName = this.lakeService.GetLakeName(model.SelectedLakeId);
                        var path = Server.MapPath(
                            $"{Constants.ImageGalleriesBaseServerFolder}/{lakeName}/");

                        directoryHelper.CreateIfNotExist(path);

                        var url = $"{Constants.ImageGalleriesBaseFolder}/{lakeName}/{file.FileName}"; 
                        file.SaveAs(Server.MapPath(url));

                        var image = this.imageFactory.CreateImage(url, date, model.ImageInfo);
                        if (User.IsInRole("Moderator"))
                        {
                            image.IsConfirmed = true;
                        }

                        var gallery = this.imageGalleryService.FindById(model.SelectedImageGalleryId);
                        if (gallery == null)
                        {
                            gallery = this.imageGalleryFactory.CreateImageGallery(model.Name, model.SelectedLakeId);
                        }

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
            model.SelectedImageGalleryId = null;
            model.Name = null;

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

        [HttpGet]
        [Authorize]
        public string GetGalleries(string lakeName)
        {
            var galleries = this.imageGalleryService.GetByLake(lakeName);

            return JsonConvert.SerializeObject(galleries);
        }

        private void LoadNames(AddImageViewModel model)
        {
            var lakes = this.lakeService.GetAll();

            model.Lakes = lakes;
        }
    }
}