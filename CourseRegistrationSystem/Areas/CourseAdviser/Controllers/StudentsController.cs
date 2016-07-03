using CourseRegistrationSystem.Areas.CourseAdviser.ViewModels;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.CourseAdviser.Controllers
{
    [SelectedTab("students")]
    public class StudentsController : Controller
    {
        // GET: CourseAdviser/Home
        public ActionResult Index()
        {
            return View(new StudentsIndex
            {
                Students = Database.Session.Query<Student>().ToList()
            });
        }

        public ActionResult ViewCourses(int id)
        {
            var student = Database.Session.Load<Student>(id);
            ViewBag.Name = student.FirstName + " " + student.LastName;
            ViewBag.Id = student.Id;

            if (student == null)
                return HttpNotFound();

            IQueryable<StudentsViewCourses> query;

            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                query = from students in Database.Session.Query<Student>()
                        join enrolled in Database.Session.Query<Enrollment>() on students.Id equals enrolled.StudentId
                        join courses in Database.Session.Query<Course>() on enrolled.CourseId equals courses.CourseId
                        where courses.Level == student.Level && courses.Semester == "First"
                        select new StudentsViewCourses
                        {
                            Courses = courses,
                            Status = enrolled,
                            Students = student
                        };
                ViewBag.Semester = "First Semester";
            }
            else
            {
                query = from students in Database.Session.Query<Student>()
                        join enrolled in Database.Session.Query<Enrollment>() on students.Id equals enrolled.StudentId
                        join courses in Database.Session.Query<Course>() on enrolled.CourseId equals courses.CourseId
                        where courses.Level == student.Level && courses.Semester == "Second"
                        select new StudentsViewCourses
                        {
                            Courses = courses,
                            Status = enrolled,
                            Students = student
                        };
                ViewBag.Semester = "Second Semester";
            }

            return View(query.ToList());
        }

        [HttpPost]
        public ActionResult Approve(int id)
        {
            var student = Database.Session.Load<Student>(id);

            string semester = null;

            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
                semester = "First";
            else
                semester = "Second";

            var hqlUpdate = "update Enrollment set status = 'Approved' where student_id = :studentId and Level = :level and Semester = :semester";
            Database.Session.CreateQuery(hqlUpdate)
                .SetParameter("studentId", id)
                .SetParameter("level", student.Level)
                .SetString("semester", semester)
                .ExecuteUpdate();

            return RedirectToAction("index");
        }
    }
}