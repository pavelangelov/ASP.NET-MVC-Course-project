using System;
using System.Web.Mvc;

using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Utils.Contracts;
using System.Linq;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class LakesController : Controller
    {
        private ILakeService lakeService;
        private ICommentFactory commentFactory;
        private IDateProvider dateProvider;

        public LakesController(
            ILakeService lakeService,
            ICommentFactory commentFactory,
            IDateProvider dateProvider)
        {
            Validator.ValidateForNull(lakeService, paramName: "lakeService");
            Validator.ValidateForNull(commentFactory, paramName: "commentFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");

            this.lakeService = lakeService;
            this.commentFactory = commentFactory;
            this.dateProvider = dateProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var lakes = this.lakeService.GetAll().GroupBy(l => l.Name.ToLower()[0]);

            return View(lakes);
        }

        [HttpGet]
        public ActionResult Details(string name)
        {
            var lake = this.lakeService.FindByName(name);

            return View(lake);
        }
        
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var date = this.dateProvider.GetDate();
                    var comment = this.commentFactory.CreateComment(model.LakeName, User.Identity.Name, model.Content, date);
                    var lake = this.lakeService.FindByName(model.LakeName);
                    lake.Comments.Add(comment);
                    this.lakeService.Save();
                    return Json(new { status = "success", message = GlobalMessages.AddCommentSuccessMessage });
                }
                catch (Exception)
                {
                    return Json(new { status = "error", message = GlobalMessages.AddCommentErrorMessage });
                }
            }

            return Json(new { status = "error", message = GlobalMessages.AddCOmentInvalidModelStateErrorMessage });
        }
    }
}