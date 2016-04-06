using CourseRegistrationSystem.Areas.Admin.ViewModels;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using NHibernate.Linq;
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
            return View(new CoursesIndex
            {
                Courses = Database.Session.Query<Course>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new CoursesNew
            {
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(CoursesNew form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var course = new Course
            {
                CourseCode = form.CourseCode,
                CourseTitle = form.CourseTitle,
                LecturerName = form.LecturerName,
                Level = form.Level,
                Semester = form.Semester,
                Credit = form.Credit,
                Type = form.Type
            };

            Database.Session.Save(course);
            return RedirectToAction("index");
        }

        public ActionResult Edit(int courseId)
        {
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            return View(new CoursesEdit
            {
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                LecturerName = course.LecturerName,
                Level = course.Level,
                Semester = course.Semester,
                Credit = course.Credit,
                Type = course.Type
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int courseId, CoursesEdit form)
        {
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            if (Database.Session.Query<Course>().Any(u => u.CourseCode == form.CourseCode && u.CourseId != courseId))
                ModelState.AddModelError("Course Code", "Course code must be unique");

            if (!ModelState.IsValid)
                return View(form);

            course.CourseCode = form.CourseCode;
            course.CourseTitle = form.CourseTitle;
            course.LecturerName = form.LecturerName;
            course.Level = form.Level;
            course.Semester = form.Semester;
            course.Credit = form.Credit;
            course.Type = form.Type;

            Database.Session.Update(course);
            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int courseId)
        {
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            Database.Session.Delete(course);
            return RedirectToAction("index");
        }
    }
}
