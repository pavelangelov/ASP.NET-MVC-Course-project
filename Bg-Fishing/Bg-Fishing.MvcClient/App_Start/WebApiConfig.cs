using System;
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
                defaults: new { controller = "SearchApi", name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddInnerComment",
                routeTemplate: "api/comments/add/{id}/{content}",
                defaults: new { controller = "CommentsApi" }
                );

            config.Routes.MapHttpRoute(
                name: "Get",
                routeTemplate: "api/comments/{name}/{page}",
                defaults: new { controller = "CommentsApi" }
                );

            config.Routes.MapHttpRoute(
                name: "GetLakeGalleries",
                routeTemplate: "api/images/galleries/{name}",
                defaults: new { controller = "ImagesApi", action = "GetAllGalleriesForLake" }
            );

            config.Routes.MapHttpRoute(
                name: "GetGalleryImages",
                routeTemplate: "api/images/gallery/{id}",
                defaults: new { controller = "ImagesApi", action = "GetImagesFromGallery" }
            );

            config.Routes.MapHttpRoute(
                name: "VideosController",
                routeTemplate: "api/videos/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
