using CourseRegistrationSystem.Areas.CourseAdviser.ViewModels;
using CourseRegistrationSystem.Controllers;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System.Linq;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.CourseAdviser.Controllers
{
    [Authorize(Roles = "course adviser")]
    [SelectedTab("courses")]
    public class CoursesController : BaseController
    {
        // GET: CourseAdviser/Courses
        public ActionResult Index()
        {
            return View(new CoursesIndex
            {
                // gets all the elective courses and stores them in the Courses variable
                Courses = Database.Session.Query<Course>().Where(c => c.Type != "Major").ToList()
            });
        }

        // GET: CourseAdviser/addinfo
        public ActionResult AddInfo(int courseId)
        {
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            return View(new CoursesAddInfo
            {
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle
            });
        }


        // handles the form submission on Add button click
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddInfo(CoursesAddInfo form, int courseId)
        {
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            course.CourseInfo = form.CourseInfo;
            Database.Session.SaveOrUpdate(course);
            Success("Course Advice Added Successfully", true);

            return RedirectToAction("index");
        }
    }
}