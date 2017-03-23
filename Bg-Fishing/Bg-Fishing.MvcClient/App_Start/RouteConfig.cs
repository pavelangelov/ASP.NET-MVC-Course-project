using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bg_Fishing.MvcClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Lakes",
                url: "lakes/{name}/{action}",
                defaults: new { controller = "Lakes", action = "Index" }
            );

            routes.MapRoute(
                name: "FishList",
                url: "fish/{action}/{name}",
                defaults: new { controller = "FishList", action = "Index", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
