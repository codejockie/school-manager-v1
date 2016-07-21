using CourseRegistrationSystem.Areas.Admin.ViewModels;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    // Controller for the Users view that is http://localhost:36152/admin/users
    public class UsersController : Controller
    {
        // GET: Admin/Users
        // presents the index.cshtml view in Admin/Views/Users folder
        public ActionResult Index()
        {
            // passes UsersIndex VM containing a list of all users in the DB to the View
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }

        // presents the new.cshtml view in Admin/Views/Users folder
        public ActionResult New()
        {
            // passes UsersNew VM to the View for adding a new course when Add User is clicked in the view
            // also passes the different Roles in the DB to the view
            return View(new UsersNew
            {
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckbox
                {
                    Id = role.Id,
                    IsChecked = false,
                    Name = role.Name
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        // Handles the post back when the Add User view form is submitted
        public ActionResult New(UsersNew form)
        {
            var user = new User(); // creates an instance/object of the User model
            SyncRoles(form.Roles, user.Roles); // calls the SyncRoles() passing the checked role on the form & user's role

            // checks if the DB contains the username specified, if true it adds
            // a model error
            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            // checks if what is typed in the form has valid data/values
            // if false it returns the view showing the errors
            if (!ModelState.IsValid)
                return View(form);

            // assigns the contents of the form fields to 
            // the model object created earlier properties
            user.Email = form.Email;
            user.Username = form.Username;
            user.SetPassword(form.Password);

            // saves the object
            Database.Session.Save(user);
            return RedirectToAction("index");
        }

        // compare with CoursesController's Edit method, same idea
        public ActionResult Edit(int id)
        {
            // same idea like in the CoursesController's Edit method
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            // passes the UsersEdit VM to the view with that particular course's
            // properties/details to be edited
            return View(new UsersEdit
            {
                Username = user.Username,
                Email = user.Email,
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckbox
                {
                    Id = role.Id,
                    IsChecked = user.Roles.Contains(role),
                    Name = role.Name
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        // compare with CoursesController's Edit [HttpPost] method, same idea
        public ActionResult Edit(int id, UsersEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            SyncRoles(form.Roles, user.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username && u.Id != id))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.Username = form.Username;
            user.Email = form.Email;
            Database.Session.Update(user);

            return RedirectToAction("index");
        }

        // presents the ResetPassword.cshtml view
        public ActionResult ResetPassword(int id)
        {
            // same idea of loading the user via its id
            // and checking if the user exists
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            // returns the View assigning the user.Username to
            // the UsersResetPassword VM Username property, this
            // allow the username to show on the view
            return View(new UsersResetPassword
            {
                Username = user.Username
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        // handles post back from the form
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            // loads the user via id
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            form.Username = user.Username; // assigns the user object username to the form username

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password); // calls the SetPassord() of the user object
            Database.Session.Update(user); // updates the user's password

            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        // same as delete() in the CoursesController
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);
            return RedirectToAction("index");
        }

        // method for syncing the roles selected when adding a user
        private void SyncRoles(IList<RoleCheckbox> checkboxes, IList<Role> roles)
        {
            // creates an object of type list of roles
            var selectedRoles = new List<Role>();

            // loops through the roles in the DB
            foreach (var role in Database.Session.Query<Role>())
            {
                // checks if the checked role exists, if true it assigns
                // the value to the variable
                var checkbox = checkboxes.Single(c => c.Id == role.Id);
                checkbox.Name = role.Name; // assigns the role name to checkbox name

                // if the checkbox is checked it adds it to the selectedRoles object
                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }

            // loops through the selectedRoles list, checking if it contains the Role
            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
                roles.Add(toAdd); // adds that role to roles parameter

            // removes role
            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);
        }
    }
}