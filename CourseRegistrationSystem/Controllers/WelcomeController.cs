using CourseRegistrationSystem.Helpers;
using CourseRegistrationSystem.Models;
using CourseRegistrationSystem.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Controllers
{
    [Authorize(Roles = "student")]
    [RoutePrefix("welcome")]
    [Route("{action=Index}/{id=}")]
    public class WelcomeController : BaseController
    {
        public WelcomeController(){}
        // GET: Welcome
        [Route]
        [Route("index")]
        public ActionResult Index()
        {
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);
            TempData["studentid"] = id;

            var model = new WelcomeIndex { FirstName = student.FirstName, Photo = student.Photo };

            return View(model);
        }

        public ActionResult Upload(WelcomeIndex form)
        {
            var student = Database.Session.Load<Student>(TempData["studentid"]);

            byte[] uploadedPhoto = new byte[form.ProfilePic.InputStream.Length];
            form.ProfilePic.InputStream.Read(uploadedPhoto, 0, uploadedPhoto.Length);

            try
            {
                student.Photo = uploadedPhoto;

                Database.Session.SaveOrUpdate(student);
                Success("Profile picture upload was successful", true);
                return RedirectToAction("index");
            }
            catch
            {
                Danger("Oops! something went wrong, we're sorry. Please try again later", true);
                return RedirectToAction("index");
            }
        }

        public ActionResult Welcome()
        {
            return PartialView("_Welcome");
        }

        public ActionResult ChangePassword()
        {
            var id = (from i in Database.Session.Query<User>()
                      where i.Username == User.Identity.Name
                      select i.Id).First();
            var user = Database.Session.Load<User>(id);

            if (user == null)
                return HttpNotFound();

            return PartialView("ChangePassword", new WelcomeChangePassword
            {
                Username = user.Username,
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(WelcomeChangePassword form)
        {
            var id = (from i in Database.Session.Query<User>()
                      where i.Username == User.Identity.Name
                      select i.Id).First();
            var user = Database.Session.Load<User>(id);

            if (user == null)
                return HttpNotFound();

            form.Username = user.Username;

            if (!ModelState.IsValid)
                return PartialView(form);

            user.SetPassword(form.Password);
            Database.Session.Update(user);

            return RedirectToAction("index");
        }

        public ActionResult Biodata()
        {
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            return PartialView("Biodata", new WelcomeBiodata
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                RegistrationNumber = student.RegistrationNumber,
                Email = student.Email,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth.ToShortDateString(),
                Gender = (Gender)Enum.Parse(typeof(Gender), student.Gender),
                SponsorName = student.SponsorName,
                SponsorPhone = student.SponsorPhone,
                State = (States)Enum.Parse(typeof(States), student.State),
                LGA = student.LGA,
                Hometown = student.Hometown,
                Country = (Country)Enum.Parse(typeof(Country), student.Country),
                CourseOfStudy = student.CourseOfStudy,
                Department = student.Department,
                Level = (Level)Enum.Parse(typeof(Level), student.Level),
                BloodGroup = (BloodGroup)Enum.Parse(typeof(BloodGroup), student.BloodGroup),
                Genotype = (Genotype)Enum.Parse(typeof(Genotype), student.Genotype),
                Disability = (Disability)Enum.Parse(typeof(Disability), student.Disability)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Biodata(WelcomeBiodata form)
        {
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
                return PartialView(form);

            student.Email = form.Email;
            student.Address = form.Address;
            student.PhoneNumber = form.PhoneNumber;
            student.BloodGroup = form.BloodGroup.ToString();
            student.Genotype = form.Genotype.ToString();
            student.Disability = form.Disability.ToString();

            Database.Session.SaveOrUpdate(student);

            return RedirectToAction("index");
        }

        public ActionResult RegisterCourse()
        {
            List<Course> courses;
            var model = new CourseSelectionViewModel();

            var id = Database.Session.Query<Student>()
                .Where(s => s.RegistrationNumber == User.Identity.Name)
                .Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.MiddleName = student.MiddleName;
            model.RegistrationNumber = student.RegistrationNumber;
            model.Department = student.Department;
            model.Level = student.Level;
            model.StudentType = student.StudentType;

            string studentLevel = null;

            switch (student.Level)
            {
                case "LevelOne": studentLevel = "100"; break;
                case "LevelTwo": studentLevel = "200"; break;
                case "LevelThree": studentLevel = "300"; break;
                case "LevelFour": studentLevel = "400"; break;
                case "LevelFive": studentLevel = "500"; break;
            }

            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                courses = Database.Session.Query<Course>()
                    .Where(c => c.Level == studentLevel && c.Semester == "First").ToList();
            }
            else
            {
                courses = Database.Session.Query<Course>()
                    .Where(c => c.Level == studentLevel && c.Semester == "Second").ToList();
            }

            foreach (var course in courses)
            {
                model.Courses.Add(new CourseViewModel
                {
                    Id = course.CourseId,
                    CourseCode = course.CourseCode,
                    CourseTitle = course.CourseTitle,
                    Credit = course.Credit,
                    Type = course.Type,
                    IsSelected = false
                });
            }

            string semester = null;
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
                semester = "First";
            else
                semester = "Second";

            var registeredApproved = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == studentLevel && x.Semester == semester)
                .Any(x => x.Status == "Approved");

            var registeredPending = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == studentLevel && x.Semester == semester)
                .Any(x => x.Status == "Pending");

            if (registeredApproved)
            {
                return PartialView("_Approved");
            }
            else if (registeredPending)
            {
                return PartialView("_Pending");
            }
            return PartialView("RegisterCourse", model);
        }

        [HttpPost]
        public ActionResult RegisterCourse(CourseSelectionViewModel form)
        {
            var courses = Database.Session.Query<Course>().ToList();

            var id = Database.Session.Query<Student>()
                .Where(s => s.RegistrationNumber == User.Identity.Name)
                .Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            var selectedIds = form.getSelectedIds();

            // Use the ids to retrieve the records for the selected course
            // from the database:
            var selectedCourse = from x in courses
                                 where selectedIds.Contains(x.CourseId)
                                 select x;

            //// Process according to your requirements:
            try
            {
                foreach (var course in selectedCourse)
                {
                    var enrollment = new Enrollment
                    {
                        CourseId = course.CourseId,
                        StudentId = student.Id,
                        Level = course.Level,
                        Semester = course.Semester,
                        Status = "Pending"
                    };
                    Database.Session.Save(enrollment);
                }
                Success("Registration successful", true);
                return RedirectToAction("index");
            }
            catch
            {
                Danger("Oops! We are sorry, an error occurred while processing your request.\nPlease try again later");
                return RedirectToAction("index");
            }
        }

        public ActionResult Rule()
        {
            return View();
        }

        public ActionResult ViewRegistration()
        {
            var model = new WelcomeViewRegistration();

            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            IQueryable<EnrolledCourses> query;

            string studentLevel = null;

            switch (student.Level)
            {
                case "LevelOne": studentLevel = "100"; break;
                case "LevelTwo": studentLevel = "200"; break;
                case "LevelThree": studentLevel = "300"; break;
                case "LevelFour": studentLevel = "400"; break;
                case "LevelFive": studentLevel = "500"; break;
            }

            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.MiddleName = student.MiddleName;
            model.RegistrationNumber = student.RegistrationNumber;
            model.Department = student.Department;
            model.Level = studentLevel;
            model.Address = student.Address;
            model.Email = student.Email;
            model.DateOfBirth = student.DateOfBirth.ToShortDateString();
            model.Department = student.Department;
            model.CourseOfStudy = student.CourseOfStudy;
            model.Gender = student.Gender;
            model.Hometown = student.Hometown;
            model.LGA = student.LGA;
            model.PhoneNumber = student.PhoneNumber;
            model.Photo = student.Photo;
            model.SponsorName = student.SponsorName;
            model.SponsorPhone = student.SponsorPhone;
            model.State = student.State;
            model.StudentType = student.StudentType;

            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                query = from students in Database.Session.Query<Student>()
                        join enrolled in Database.Session.Query<Enrollment>() on students.Id equals enrolled.StudentId
                        join courses in Database.Session.Query<Course>() on enrolled.CourseId equals courses.CourseId
                        where courses.Level == studentLevel && courses.Semester == "First"
                        select new EnrolledCourses
                        {
                            Courses = courses,
                            Enrolled = enrolled
                        };
            }
            else
            {
                query = from students in Database.Session.Query<Student>()
                        join enrolled in Database.Session.Query<Enrollment>() on students.Id equals enrolled.StudentId
                        join courses in Database.Session.Query<Course>() on enrolled.CourseId equals courses.CourseId
                        where courses.Level == studentLevel && courses.Semester == "Second"
                        select new EnrolledCourses
                        {
                            Courses = courses,
                            Enrolled = enrolled
                        };
            }

            model.Enroll = query.ToList();

            string semester = null;
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
                semester = "First";
            else
                semester = "Second";

            var registeredApproved = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == studentLevel && x.Semester == semester)
                .Any(x => x.Status == "Approved");

            if (registeredApproved)
            {
                return PartialView("ApprovedRegistration", model);
            }

            return PartialView(model);
        }
    }
}