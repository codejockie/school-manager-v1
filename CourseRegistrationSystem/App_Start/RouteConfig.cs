using CourseRegistrationSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CourseRegistrationSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            var namespaces = new[] { typeof(HomeController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);

            routes.MapRoute("AdminLogin", "adminlogin", new { controller = "Auth", action = "AdminLogin" }, namespaces);
            routes.MapRoute("AdminLogout", "adminlogout", new { controller = "Auth", action = "AdminLogout" }, namespaces);

            routes.MapRoute("Register", "register", new { controller = "Auth", action = "Register" }, namespaces);

            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" }, namespaces);
        }
    }
}
