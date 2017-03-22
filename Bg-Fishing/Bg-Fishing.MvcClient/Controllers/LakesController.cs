using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}