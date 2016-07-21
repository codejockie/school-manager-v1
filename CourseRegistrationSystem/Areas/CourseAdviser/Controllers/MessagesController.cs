using CourseRegistrationSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.CourseAdviser.Controllers
{
    [Authorize(Roles = "course adviser")]
    [SelectedTab("messages")]
    public class MessagesController : Controller
    {
        // GET: CourseAdviser/Messages
        public ActionResult Index()
        {
            return View();
        }
    }
}