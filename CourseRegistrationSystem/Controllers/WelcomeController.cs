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
    [Authorize(Roles = "user")]
    [RoutePrefix("welcome")]
    [Route("{action=Index}/{id=}")]
    public class WelcomeController : Controller
    {
        public WelcomeController()
        {

        }
        // GET: Welcome
        [Route]
        [Route("index")]
        public ActionResult Index()
        {
            return View(new WelcomeIndex
            {
            });
        }

        public ActionResult Welcome()
        {
            return PartialView("_Welcome");
        }

        public ActionResult ChangePassword()
        {
            var query = from i in Database.Session.Query<User>()
                        where i.Username == User.Identity.Name
                        select i.Id;
            var id = query.First();
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return PartialView("_ChangePassword", new WelcomeChangePassword
            {
                Username = user.Username,
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(WelcomeChangePassword form)
        {
            var query = from i in Database.Session.Query<User>()
                        where i.Username == User.Identity.Name
                        select i.Id;
            var id = query.First();
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            form.Username = user.Username;

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);
            Database.Session.Update(user);

            return RedirectToAction("index");
        }

        public ActionResult Biodata()
        {
            ViewBag.State = new SelectList(Database.Session.Query<State>().ToList(), "Id", "Name");

            var query = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id);
            var id = query.First();
            var student = Database.Session.Load<Student>(id);

            return PartialView("_Biodata", new WelcomeBiodata
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                RegistrationNumber = student.RegistrationNumber,
                Email = student.Email,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth.Date,
                Gender = student.Gender,
                State = student.State,
                LGA = student.LGA,
                Hometown = student.Hometown,
                Nationality = student.Nationality,
                CourseOfStudy = student.CourseOfStudy,
                Department = student.Department,
                Level = student.Level,
                BloodGroup = student.BloodGroup,
                Genotype = student.Genotype,
                Disability = student.Disability
            });
        }

        public PartialViewResult PreRegistration()
        {
            return PartialView("_PreRegistration");
        }

        public ActionResult RegisterCourse()
        {
            return PartialView("_RegisterCourse");
        }

        public ActionResult Compose()
        {
            return PartialView("_Compose");
        }

        public ActionResult Inbox()
        {
            return PartialView("_Inbox");
        }
    }
}