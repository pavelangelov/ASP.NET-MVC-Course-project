using System.Web.Mvc;

using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class LakesController : Controller
    {
        private ILakeService lakeService;

        public LakesController(ILakeService lakeService)
        {
            Validator.ValidateForNull(lakeService, paramName: "lakeService");

            this.lakeService = lakeService;
        }

        [HttpGet]
        public ActionResult Index(string name)
        {
            var lake = this.lakeService.FindByName(name);

            return View("Index", model: lake);
        }

        [HttpGet]
        public ActionResult Comments(string lakeName, int page)
        {
            var lake = this.lakeService.FindByName(lakeName);

            return View(model: lake);
        }

        [HttpPost]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add new comment
            }

            // TODO: Show model errors
            return View();
        }
    }
}