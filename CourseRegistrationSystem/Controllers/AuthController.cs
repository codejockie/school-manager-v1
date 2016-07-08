using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using CourseRegistrationSystem.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CourseRegistrationSystem.Controllers
{
    public class AuthController : BaseController
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("login");
        }

        [SelectedTab("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null)
                CourseRegistrationSystem.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("index", "welcome");
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AuthAdminLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null)
                CourseRegistrationSystem.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            if (User.Identity.IsAuthenticated && User.IsInRole("courseadviser"))
            {
                return RedirectToAction("index", new { controller = "students", area = "courseadviser" });
            }
            return RedirectToAction("index", new { controller = "users", area = "admin" });
        }

        public ActionResult AdminLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        [SelectedTab("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(AuthRegister form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            TempData["firstname"] = form.FirstName;
            TempData["lastname"] = form.LastName;
            TempData["middlename"] = form.MiddleName;
            TempData["address"] = form.Address;
            TempData["matricno"] = form.RegistrationNumber;
            TempData["newmatricno"] = form.RegistrationNumber;
            TempData["phone"] = form.PhoneNumber;
            TempData["email"] = form.Email;
            TempData["newemail"] = form.Email;
            TempData["dob"] = DateTime.Parse(form.DateOfBirth);
            TempData["gender"] = form.Gender.ToString();
            TempData["sponsorname"] = form.SponsorName;
            TempData["sponsorphone"] = form.SponsorPhone;
            TempData["country"] = form.Country.ToString();
            TempData["state"] = form.State.ToString();
            TempData["lga"] = form.LGA;
            TempData["hometown"] = form.Hometown;
            TempData["level"] = form.Level.ToString();
            TempData["course"] = form.CourseOfStudy;
            TempData["department"] = form.Department;
            TempData["disability"] = form.Disability.ToString();
            TempData["bloodgroup"] = form.BloodGroup.ToString();
            TempData["genotype"] = form.Genotype.ToString();
            TempData["studenttype"] = form.StudentType.ToString();

            byte[] uploadedPhoto = new byte[form.Photo.InputStream.Length];
            form.Photo.InputStream.Read(uploadedPhoto, 0, uploadedPhoto.Length);
            TempData["uploadedphoto"] = uploadedPhoto;

            return RedirectToAction("newstudent", "auth");
        }

        public ActionResult NewStudent()
        {
            AuthNewStudent newStudent = null;

            if (TempData["matricno"] != null && TempData["email"] != null)
            {
                newStudent = new AuthNewStudent
                {
                    Username = TempData["matricno"].ToString(),
                    Email = TempData["email"].ToString()
                };
            }

            return View(newStudent);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewStudent(AuthNewStudent form)
        {
            var user = new User();
            var student = new Student();

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            try
            {
                student.FirstName = TempData["firstname"].ToString();
                student.LastName = TempData["lastname"].ToString();
                student.MiddleName = TempData["middlename"].ToString();
                student.Address = TempData["address"].ToString();
                student.RegistrationNumber = TempData["newmatricno"].ToString();
                student.PhoneNumber = TempData["phone"].ToString();
                student.Email = TempData["newemail"].ToString();
                student.DateOfBirth = Convert.ToDateTime(TempData["dob"]);
                student.Gender = TempData["gender"].ToString();
                student.SponsorName = TempData["sponsorname"].ToString();
                student.SponsorPhone = TempData["sponsorphone"].ToString();
                student.Country = TempData["country"].ToString();
                student.State = TempData["state"].ToString();
                student.LGA = TempData["lga"].ToString();
                student.Hometown = TempData["hometown"].ToString();
                student.Level = TempData["level"].ToString();
                student.CourseOfStudy = TempData["course"].ToString();
                student.Department = TempData["department"].ToString();
                student.Disability = TempData["disability"].ToString();
                student.BloodGroup = TempData["bloodgroup"].ToString();
                student.Genotype = TempData["genotype"].ToString();
                student.StudentType = TempData["studenttype"].ToString();
                student.Photo = (byte[])TempData["uploadedphoto"];

                user.Email = form.Email;
                user.Username = form.Username;
                user.SetPassword(form.Password);

                Database.Session.Save(student);
                Database.Session.Save(user);
                int lastUserId = Database.Session.Query<User>().Max(x => x.Id);

                var roleUser = new RoleUsers
                {
                    UserId = lastUserId,
                    RoleId = 3
                };
                Database.Session.Save(roleUser);

                Success("Registration was Successful!", true);
            }
            catch (Exception)
            {
                Danger("Oops!, we're sorry, something went wrong. Please try again later", true);
                return RedirectToAction("register", "auth");
            }

            return RedirectToAction("register", "auth");
        }
    }
}