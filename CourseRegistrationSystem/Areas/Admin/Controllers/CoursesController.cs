using CourseRegistrationSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("courses")]
    public class CoursesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
