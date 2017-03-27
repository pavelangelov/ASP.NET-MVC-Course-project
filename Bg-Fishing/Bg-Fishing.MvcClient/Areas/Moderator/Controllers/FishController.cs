using System;
using System.Web;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class FishController : ModeratorBaseController
    {
        public const int ImageMaxSize = 3 * 1024 * 1000;
        public const string FishImagesFolder = "/Images/Fish/";
        public const string FishAddedSuccessKey = "FishAddedSuccess";
        public const string FishAddedSuccessMessage = "Рибата е добавена успешно";
        public const string FishAddingErrorMessage = "Не е избрана снимка или размера на снимката е по-голям от 3 MB!";
        public const string FishAddingFailMessage = "Грешка при добавянето на рибата!";

        private IFishFactory fishFactory;
        private IFishService fishService;

        public FishController(IFishFactory fishFactory, IFishService fishService)
        {
            Validator.ValidateForNull(fishFactory, paramName: "fishFactory");
            Validator.ValidateForNull(fishService, paramName: "fishService");

            this.fishFactory = fishFactory;
            this.fishService = fishService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new AddFishViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(AddFishViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.ContentLength <= ImageMaxSize)
                    {
                        var filePath = FishImagesFolder + file.FileName; 
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Fish/")
                                                              + file.FileName);

                        var fish = this.fishFactory.CreateFish(model.FishName, model.FishType, filePath, model.Info);
                        this.fishService.Add(fish);
                        this.fishService.Save();

                        TempData[FishAddedSuccessKey] = FishAddedSuccessMessage;
                    }
                    else
                    {
                        ModelState.AddModelError("", FishAddingErrorMessage);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", FishAddingFailMessage);
                }

                return View(model);
            }

            return View(model);
        }
    }
}