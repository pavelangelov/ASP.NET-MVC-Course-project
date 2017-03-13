using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator
{
    [Authorize(Roles = "Moderator")]
    public class ModeratorBaseController : Controller
    {
    }
}