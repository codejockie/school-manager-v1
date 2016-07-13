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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rule()
        {
            return View();
        }
    }
}
