using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.CourseAdviser.Controllers
{
    public class HomeController : Controller
    {
        // GET: CourseAdviser/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}