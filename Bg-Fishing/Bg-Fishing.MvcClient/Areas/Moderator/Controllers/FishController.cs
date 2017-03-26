using System;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class FishController : ModeratorBaseController
    {
        public const int ImageMaxSize = 3 * 1024 * 1000;

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
                    if (file != null && file.ContentLength < ImageMaxSize)
                    {
                        var filePath = "/Images/Fish/" + file.FileName; 
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Fish/")
                                                              + file.FileName);

                        var fish = this.fishFactory.CreateFish(model.FishName, model.FishType, filePath, model.Info);
                        this.fishService.Add(fish);
                        this.fishService.Save();

                        TempData["FishAddedSuccess"] = "Рибата е добавена успешно";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не е избрана снимка или размера на снимката е по-голям от 3 MB!");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Грешка при добавянето на рибата!");
                }

                return View(model);
            }

            return View(model);
        }
    }
}