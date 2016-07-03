using CourseRegistrationSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Controllers
{
    [SelectedTab("home")]
    // The HomeController represents the homepage ie. http://localhost:36152/home
    public class HomeController : BaseController
    {
        // This method returns the index page ie. the home/landing page
        public ActionResult Index()
        {
            return View();
        }

        // This method returns the rule page when navigated to ie. http://localhost:36152/rule
        public ActionResult Rule()
        {
            return View();
        }
    }
}
