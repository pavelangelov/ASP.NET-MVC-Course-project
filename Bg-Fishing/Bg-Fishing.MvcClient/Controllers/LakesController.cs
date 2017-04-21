using System;
using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;
using Bg_Fishing.MvcClient.Models;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class LakesController : Controller
    {
        public const int ShowedComments = 5;

        private ILakeService lakeService;
        private ICommentFactory commentFactory;
        private IDateProvider dateProvider;
        private ICommentService commentsService;

        public LakesController(
            ILakeService lakeService,
            ICommentFactory commentFactory,
            IDateProvider dateProvider,
            ICommentService commentsService)
        {
            Validator.ValidateForNull(lakeService, paramName: "lakeService");
            Validator.ValidateForNull(commentFactory, paramName: "commentFactory");
            Validator.ValidateForNull(dateProvider, paramName: "dateProvider");
            Validator.ValidateForNull(commentsService, paramName: "commentsService");

            this.lakeService = lakeService;
            this.commentFactory = commentFactory;
            this.dateProvider = dateProvider;
            this.commentsService = commentsService;
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

        [HttpGet]
        public ActionResult GetComments(string name, int page = 0)
        {
            var comments = this.commentsService.GetCommentsByLakeName(name, page * ShowedComments, ShowedComments);
            var commentsCount = this.commentsService.GetCommentsCount(name);

            var hasPrev = page > 0;
            var hasNext = comments.Count() == ShowedComments && commentsCount > page * ShowedComments + ShowedComments;

            var model = new GetCommentsViewModel();
            model.Comments = comments;
            model.LakeName = name;

            if (hasPrev)
            {
                model.HasPrev = hasPrev;
                model.PrevPage = page - 1;
            }

            if (hasNext)
            {
                model.HasNext = hasNext;
                model.NextPage = page + 1;
            }

            return PartialView("_CommentsPartial", model);
        }
    }
}