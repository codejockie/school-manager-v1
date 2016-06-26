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
    public class AuthController : Controller
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
                return View(form);
            }

            byte[] uploadedPhoto = new byte[form.Photo.InputStream.Length];
            form.Photo.InputStream.Read(uploadedPhoto, 0, uploadedPhoto.Length); //TODO: pass the byte array to model and store in the Db
            ViewBag.Message = "Registration was successful";
            return View();
        }
    }
}