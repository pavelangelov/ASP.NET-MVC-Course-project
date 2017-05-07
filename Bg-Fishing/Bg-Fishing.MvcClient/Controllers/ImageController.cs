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
        private IImageFactory imageFactory;
        private IDateProvider dateProvider;
        private ILakeService lakeService;

        public ImageController(IImageFactory imageFactory, IDateProvider dateProvider, ILakeService lakeService)
        {
            Validator.ValidateForNull(imageFactory, paramName: "imageFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");
            Validator.ValidateForNull(lakeService, paramName: "lakeService");

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
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    var date = this.dateProvider.GetDate();
                    var url = file.FileName; // TODO: fix this!!!
                    var image = this.imageFactory.CreateImage(url, date, model.ImageInfo);
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