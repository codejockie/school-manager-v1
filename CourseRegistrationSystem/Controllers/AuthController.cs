using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using CourseRegistrationSystem.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
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
        public ActionResult AdminLogin(AuthLogin form, string returnUrl)
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
                Danger("Looks like something went wrong. Please check your form.", true);
                return View(form);
            }

            TempData["matricno"] = form.RegistrationNumber;
            TempData["email"] = form.Email;
            byte[] uploadedPhoto = new byte[form.Photo.InputStream.Length];
            form.Photo.InputStream.Read(uploadedPhoto, 0, uploadedPhoto.Length); //TODO: pass the byte array to model and store in the Db
            
            //int lastStudentId = Database.Session.Query<Student>().Max(x => x.Id);
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

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.Email = form.Email;
            user.Username = form.Username;
            user.SetPassword(form.Password);

            Database.Session.Save(user);
            return View(); // TODO: add the code to insert a new user to the users table
        }
    }
}