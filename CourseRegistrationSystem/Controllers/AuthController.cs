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
    // This Controller is used for all authentication purposes ie. the logging system
    public class AuthController : BaseController
    {
        // This method signs a user out once he/she clicks logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("login");
        }

        // This method returns the login page/view when Login link is clicked on the home page ie.
        // http://localhost:36152/login, it presents the login View to the user
        public ActionResult Login()
        {
            return View();
        }


        // This method handles the user input ie. it collects what the user typed in the view that was
        // presented and uses the information like username/password to check against the DB
        // Notice how the AuthLogin viewmodel is passed as a parameter to this method, doing that
        // enables this method to be able to retrieve what has been typed by the user.
        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            // Query the DB to check if the username entered by the user exist
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null) //if not found, it fakes an hash to prevent Time Attack
                CourseRegistrationSystem.Models.User.FakeHash();

            // checks if the user's password and user name is correct ie. exists
            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect");

            // checks if the user filled the form correctly, if not it doesn't permit submission,
            // then returns the form again showing the errors
            if (!ModelState.IsValid)
                return View(form);

            // Signs the user into the system
            FormsAuthentication.SetAuthCookie(user.Username, true);

            // checks if the user was redirected to the Login page due to lacking authentication
            // if true, the user is redirected back to the page he was
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            // On login, the user is redirected to the Welcom controller, portal
            return RedirectToAction("index", "welcome");
        }

        // Same as Login but for the Admin/Course Adviser
        public ActionResult AdminLogin()
        {
            return View();
        }

        // Same as the second Login method, but for the Admin/Course Adviser
        // Here the AuthAdminLogin viewmodel is passed as a parameter
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

            // checks to ascertain if the user attempting login is a course adviser
            // if true, he's taken to the Course Adviser's area
            if (Roles.IsUserInRole(form.Username, "course adviser"))
            {
                return RedirectToAction("index", "students", new { area = "courseadviser" });
            }

            // if otherwise, he is taken to the Admin's area
            return RedirectToAction("index", "users", new { area = "admin" });
        }

        // handles admin logout
        public ActionResult AdminLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("adminlogin");
        }
    }
}