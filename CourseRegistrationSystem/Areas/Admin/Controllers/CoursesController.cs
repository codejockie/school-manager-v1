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
    [Authorize(Roles = "admin")] // prevents access to this page by an unauthorised user, only an "admin" is allowed
    [SelectedTab("courses")] // tells what controller/page is currently active - being viewed

    // Controller for the Courses view that is http://localhost:36152/admin/courses
    public class CoursesController : Controller
    {
        // presents the index.cshtml view in Admin/Views/Courses folder
        public ActionResult Index()
        {
            // passes CoursesIndex VM containing a list of all courses in the DB to the View
            return View(new CoursesIndex
            {
                Courses = Database.Session.Query<Course>().ToList()
            });
        }

        // presents the new.cshtml view in Admin/Views/Courses folder
        public ActionResult New()
        {
            // passes CoursesNew VM to the View for adding a new course when Add Course is clicked in the view
            return View(new CoursesNew
            {
            });
        }

        [HttpPost, ValidateAntiForgeryToken] // ValidateAntiForgeryToken prevents Cross-Site Request Forgery attacks
        // Handles the post back when the Add Course view form is submitted
        // it is marked with HttpPost attribute
        public ActionResult New(CoursesNew form)
        {
            // checks if what is typed in the form has valid data/values
            // if false it returns the view showing the errors
            if (!ModelState.IsValid)
                return View(form);

            // creates an instance of the Course Model and assigns values
            // to its respective properties
            var course = new Course
            {
                CourseCode = form.CourseCode,
                CourseTitle = form.CourseTitle,
                Department = form.Department,
                LecturerName = form.LecturerName,
                Level = int.Parse(form.Level),
                Semester = form.Semester,
                Credit = form.Credit,
                Type = form.Type
            };

            // calls the Save method of NHibernate Session to save the data
            // passed to the course Model above to the DB
            Database.Session.Save(course);
            return RedirectToAction("index"); // after saving it redirects to the course index page
        }

        // presents the edit.cshtml view in Admin/Views/Courses folder
        // passes the course id as a parameter to enable that course with
        // that id to be edited
        public ActionResult Edit(int courseId)
        {
            var course = Database.Session.Load<Course>(courseId); // loads the course that have that id from the DB
            if (course == null)
                return HttpNotFound(); // same checking if the course exist in the database

            // passes the CoursesEdit VM to the view with that particular course's
            // properties/details to be edited
            return View(new CoursesEdit
            {
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                Department = course.Department,
                LecturerName = course.LecturerName,
                Level = course.Level.ToString(),
                Semester = course.Semester,
                Credit = course.Credit,
                Type = course.Type
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        // handles the post back after the edit course form has been submitted
        // same course's id and the CoursesEdit VM are passed as parameters
        public ActionResult Edit(int courseId, CoursesEdit form)
        {
            // lines 97 and 98 is the same as lines 73 and 74
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            // checks if the course if the edited course's code exist in the Db and it's not having its course id
            // if true a model error is thrown and it's shown on the form
            if (Database.Session.Query<Course>().Any(u => u.CourseCode == form.CourseCode && u.CourseId != courseId))
                ModelState.AddModelError("Course Code", "Course code must be unique");

            if (!ModelState.IsValid)
                return View(form); // same as line 46

            // assign the contents of the respective form's input fields to
            // their corresponding course model properties
            course.CourseCode = form.CourseCode;
            course.CourseTitle = form.CourseTitle;
            course.Department = form.Department;
            course.LecturerName = form.LecturerName;
            course.Level = int.Parse(form.Level);
            course.Semester = form.Semester;
            course.Credit = form.Credit;
            course.Type = form.Type;

            // calls Update() method of NHibernate Session,
            // passing the course model as a parameter
            Database.Session.Update(course);
            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        // handles the delete functionality when delete is clicked on the page
        public ActionResult Delete(int courseId)
        {
            // lines 130 and 131 same as lines 73 and 74
            var course = Database.Session.Load<Course>(courseId);
            if (course == null)
                return HttpNotFound();

            // calls Delete() of NHibernate Session,
            // passing the course model as parameter
            Database.Session.Delete(course);
            return RedirectToAction("index");
        }
    }
}
