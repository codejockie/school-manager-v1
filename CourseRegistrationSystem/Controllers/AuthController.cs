using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return Content("<h2>Login!</h2>");
        }
    }
}