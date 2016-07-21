using CourseRegistrationSystem.Areas.CourseAdviser.ViewModels;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.CourseAdviser.Controllers
{
    [Authorize(Roles = "course adviser")] // ensures that only a course adviser can view this controller's methods/views
    [SelectedTab("students")]
    public class StudentsController : Controller
    {
        // GET: CourseAdviser/Home
        public ActionResult Index()
        {
            // passes the StudentsIndex VM to the Index view
            return View(new StudentsIndex
            {
                // assigns the list of all students in the DB to Students property
                // of the StudentsIndex VM
                Students = Database.Session.Query<Student>().ToList()
            });
        }

        // GET: courseadviser/viewcourses/id?
        // presents the view for a particular student's registered courses
        // the id parameter represents the student's id in the DB
        public ActionResult ViewCourses(int id)
        {
            var student = Database.Session.Load<Student>(id); // loads the student with that id and assigns it to student
            // concatenates the student's firsname and lastname, then assigns it to ViewBag.Name
            // ViewBag is a dynamic property
            ViewBag.Name = student.FirstName + " " + student.LastName;
            //  assigns the student's id to ViewBag.Id
            ViewBag.Id = student.Id;

            if (student == null)
                return HttpNotFound(); // returns HttpNotFound if the the student been loaded doesn't exist

            IQueryable<StudentsViewCourses> query; // creates an IQueryable of StudentsViewCourses variable

            // determines if it first/second semester
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                // when first, assign all first semester courses based on the student's level to the qurey variable
                // it assign values to StudentsViewCourses VM properties (Courses, Status, Students)
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
                // when second, assign all first semester courses based on the student's level to the qurey variable
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

            // passes the query as a list to View
            return View(query.ToList());
        }

        // this action/method is handles post back when the
        // adviser clicks on the Approve button
        [HttpPost]
        public ActionResult Approve(int id)
        {
            var student = Database.Session.Load<Student>(id); // loads a student with that id passed as a parameter

            string semester = null; // creates a variable semester

            // determines the semeter and assign value to the semester
            // variable accordingly
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
                semester = "First";
            else
                semester = "Second";

            // assigns a SQL UPDATE query to the hqlUpdate variable
            // the query is of the NHibernate type HQL (Hibernate Query Language)
            // the variables in the query that start with a : like :studentId
            // are placeholders/parameters that are replaced during execution
            var hqlUpdate = "update Enrollment set status = 'Approved' where student_id = :studentId and Level = :level and Semester = :semester";

            // calls the NHibernate CreateQuery() passing the hqlUpdate variable
            // as an argument
            Database.Session.CreateQuery(hqlUpdate)
                .SetParameter("studentId", id) // here the :studentId is replaced with id
                .SetParameter("level", student.Level) // here :level is replaced with student's level
                .SetString("semester", semester) // here :semester is replaced with current semester
                .ExecuteUpdate(); // calls the ExecuteUpdate() to execute the query

            return RedirectToAction("index");
        }
    }
}