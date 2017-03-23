using System.Web.Mvc;

using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class LakesController : Controller
    {
        private ILakeService lakeService;
        private ICommentService commentService;
        private ICommentFactory commentFactory;
        private IDateProvider dateProvider;

        public LakesController(
            ILakeService lakeService,
            ICommentService commentService,
            ICommentFactory commentFactory,
            IDateProvider dateProvider)
        {
            Validator.ValidateForNull(lakeService, paramName: "lakeService");
            Validator.ValidateForNull(commentService, paramName: "commentService");
            Validator.ValidateForNull(commentFactory, paramName: "commentFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");

            this.lakeService = lakeService;
            this.commentService = commentService;
            this.commentFactory = commentFactory;
            this.dateProvider = dateProvider;
        }

        [HttpGet]
        public ActionResult Index(string name)
        {
            var lake = this.lakeService.FindByName(name);

            return View("Index", model: lake);
        }

        [HttpGet]
        public ActionResult Comments(string name, int page)
        {
            var comments = this.commentService.GetAllByLakeName(name);

            return Json(new { comments = comments }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [Authorize]
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
                catch (System.Exception ex)
                {
                    return Json(new { status = "error", message = GlobalMessages.AddCommentErrorMessage });
                }
            }

            // TODO: Show model errors
            return Json(new { status = "error", message = GlobalMessages.AddCOmentInvalidModelStateErrorMessage });
        }
    }
}