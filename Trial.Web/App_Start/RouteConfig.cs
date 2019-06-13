﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trial.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{*path}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"Trial.Web.Controllers"}
            );

            /*routes.MapRoute(
                name: "Blog",
                url: "blog/{path}",
                defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional },
                namespaces: new[] { "Trial.Web.Controllers" }
            );*/
        }
    }
}
