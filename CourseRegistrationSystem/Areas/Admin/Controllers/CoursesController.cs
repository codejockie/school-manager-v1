using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.Admin.Controllers
{
    public class CoursesController : Controller
    {
        public ActionResult Index()
        {
            return Content("<h2>Courses</h2>");
        }
    }
}
