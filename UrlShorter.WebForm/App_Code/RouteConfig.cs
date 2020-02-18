using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace UrlShorter.WebForm
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);
            //routes.MapPageRoute("default_all", "{anything}", "~/Default/{anything}");
            //routes.MapPageRoute("default_all", "{anything}", "~/Default.aspx",
            //    false, null,
            //    new RouteValueDictionary { { "anything", "[0-9]*" } });
            routes.MapPageRoute("Default", "{symbol}", "~/Default.aspx");
        }
    }
}