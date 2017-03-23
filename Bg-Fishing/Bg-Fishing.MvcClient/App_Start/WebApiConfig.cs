﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bg_Fishing.MvcClient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Search",
                routeTemplate: "api/search/{action}/{name}",
                defaults: new { controller = "Search", name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Get",
                routeTemplate: "api/comments/{name}/{page}",
                defaults: new { controller = "Comments" }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
