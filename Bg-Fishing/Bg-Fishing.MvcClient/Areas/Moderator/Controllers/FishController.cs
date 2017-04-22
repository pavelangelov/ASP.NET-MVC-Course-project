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
                    if (file != null && file.ContentLength <= Constants.ImageMaxSize)
                    {
                        var filePath = Constants.FishImagesFolder + file.FileName; 
                        file.SaveAs(HttpContext.Server.MapPath(Constants.FishImagesServerFolder)
                                                              + file.FileName);

                        var fish = this.fishFactory.CreateFish(model.FishName, model.FishType, filePath, model.Info);
                        this.fishService.Add(fish);
                        this.fishService.Save();

                        TempData[GlobalMessages.FishAddedSuccessKey] = GlobalMessages.FishAddedSuccessMessage;
                    }
                    else
                    {
                        ModelState.AddModelError("", GlobalMessages.FishAddingErrorMessage);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", GlobalMessages.FishAddingFailMessage);
                }

                return View(model);
            }

            return View(model);
        }
    }
}