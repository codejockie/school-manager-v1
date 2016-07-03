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
    [Authorize(Roles = "student")] // prevents access to this page by an unauthorised user
    [RoutePrefix("welcome")] // routes the url http://localhost:36152/welcome
    [Route("{action=Index}/{id=}")] // tells routing engine that default page is Index.cshtml and also that the id is optional ie. http://localhost:36152/index/id
    public class WelcomeController : BaseController // handles the Welcome page ie. the student profile
    {
        public WelcomeController(){}
        // GET: Welcome
        [Route]
        [Route("index")] // http://localhost:36152/index/id => index.cshtml in the Views' Welcome folder
        public ActionResult Index()
        {
            // checks the Student table in the DB for the reg no. of the student that matches the
            // username of the signed in student, if true it returns the id of that student
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();

            // loads the student with that id and stores it in the student variable
            var student = Database.Session.Load<Student>(id);

            // also stores it in TempData dictionary to be used by another action/method
            TempData["studentid"] = id;

            // creates an object of the WelcomeIndex viewmodel so data can be passed to it
            // data passed is made available in the View/page
            // the Firstname, Photo are passed the student's firstname and photo
            var model = new WelcomeIndex { FirstName = student.FirstName, Photo = student.Photo };

            // passes the model object to the View and returns that View
            return View(model);
        }

        // handles student profile pix upload that is shown in the welcome/index.cshtml view
        // the WelcomeIndex VM is passed as a parameter to this method
        [HttpPost]
        public ActionResult Upload(WelcomeIndex form)
        {
            // recall the student's id stored in the TempData dictionary? Guess what?
            // it is used here to load the student again
            var student = Database.Session.Load<Student>(TempData["studentid"]);

            // creates a byte array to hold the byte conversion of the uploaded pix
            // [form.ProfilePic.InputStream.Length] converts the pix ie. form.ProfilePic to bytes
            byte[] uploadedPhoto = new byte[form.ProfilePic.InputStream.Length];

            // reads the content of the file (converted byte pix)
            form.ProfilePic.InputStream.Read(uploadedPhoto, 0, uploadedPhoto.Length);

            try
            {
                // assigns the array to our Student model's photo property
                student.Photo = uploadedPhoto;

                Database.Session.SaveOrUpdate(student); // saves it to the DB
                // calls the Success method in our base controller to show a success message
                Success("Profile picture upload was successful", true);

                return RedirectToAction("index"); // redirects to the index page of WelcomeController
            }
            catch
            {
                // catches any error that might occur,
                // calls the Danger method in our base controller to show a failure message
                Danger("Oops! something went wrong, we're sorry. Please try again later", true);
                return RedirectToAction("index");
            }
        }

        // presents the view ChangePassword.cshtml
        public ActionResult ChangePassword()
        {
            // Gets the id of the logged in student
            var id = (from i in Database.Session.Query<User>()
                      where i.Username == User.Identity.Name
                      select i.Id).First();
            var user = Database.Session.Load<User>(id); // loads the student, stores in user variable

            if (user == null) // if user doesn't exist, it returns HttpNotFound()
                return HttpNotFound();

            // passes the WelcomeChangePassword VM to the View
            return PartialView();
        }

        // handles post back from the view ie. when a user submits the form to change password
        // WelcomeChangePassword is passed as a parameter
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(WelcomeChangePassword form)
        {
            // Gets the id of the logged in student
            var id = (from i in Database.Session.Query<User>()
                      where i.Username == User.Identity.Name
                      select i.Id).First();
            var user = Database.Session.Load<User>(id); // loads the student, stores in user variable

            if (user == null) // if user doesn't exist, it returns HttpNotFound()
                return HttpNotFound();

            // checks if the user filled the form correctly, if not it doesn't permit submission,
            // then returns the form again showing the errors
            if (!ModelState.IsValid)
                return PartialView(form);

            // calls the SetPassword() on form.Password ie. user's entered password and hashes it
            user.SetPassword(form.Password);
            Database.Session.Update(user); // saves it to the DB

            return RedirectToAction("index");
        }

        // handles the Biodata.cshtml View, presents the edit form to the user
        public ActionResult Biodata()
        {
            // by now you should know what this does, don't you? Like you don't already know :)
            // check the function in the ChangePassword() method
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id); // same goes here

            if (student == null) // same here too :)
                return HttpNotFound();

            // passes the VM WelcomeBiodata to the view, so data are populated on form load
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
                Level = (Level)Enum.Parse(typeof(Level), student.Level.ToString()),
                BloodGroup = (BloodGroup)Enum.Parse(typeof(BloodGroup), student.BloodGroup),
                Genotype = (Genotype)Enum.Parse(typeof(Genotype), student.Genotype),
                Disability = (Disability)Enum.Parse(typeof(Disability), student.Disability)
            });
        }

        // handles post back, WelcomeBiodata VM is passed here
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Biodata(WelcomeBiodata form)
        {
            // same repitition for almost all the methods of this class
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
                return PartialView(form);

            // these assign their form values to their respective Student Model equivalent to persist
            // to the DB
            student.Email = form.Email;
            student.Address = form.Address;
            student.PhoneNumber = form.PhoneNumber;
            student.BloodGroup = form.BloodGroup.ToString();
            student.Genotype = form.Genotype.ToString();
            student.Disability = form.Disability.ToString();

            Database.Session.SaveOrUpdate(student); // saves/updates the Model to the DB

            return RedirectToAction("index");
        }

        // handles the RegisterCourse.cshtml View
        public ActionResult RegisterCourse()
        {
            List<Course> courses; // creates of variable of type List of Course
            // creates an object of CourseSelectionViewModel VM
            var model = new CourseSelectionViewModel();

            // same old
            var id = Database.Session.Query<Student>()
                .Where(s => s.RegistrationNumber == User.Identity.Name)
                .Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            // pass the Student Model values to its respective VM equivalent
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.MiddleName = student.MiddleName;
            model.RegistrationNumber = student.RegistrationNumber;
            model.Department = student.Department;
            model.Level = student.Level;
            model.StudentType = student.StudentType;

            // determines the semester the student want to register for
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                // loads first semester courses based on the student's level ie. the student only sees
                // the courses pertaining to his/her level and assigns the loaded list of courses to
                // the courses variable
                courses = Database.Session.Query<Course>()
                    .Where(c => c.Level == student.Level && c.Semester == "First").ToList();
            }
            else
            {
                // loads second semester courses based on the student's level ie. the student only sees
                // the courses pertaining to his/her level and assigns the loaded list of courses to
                // the courses variable
                courses = Database.Session.Query<Course>()
                    .Where(c => c.Level == student.Level && c.Semester == "Second").ToList();
            }

            // iterates through all the courses passed/assigned into the courses variable
            foreach (var course in courses)
            {
                // for each course it assigns the following to the CourseViewModel VM properties
                // then adds it to the Courses property of the CourseSelectionViewModel VM object we
                // had earlier created
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
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10) // determines the semester
                semester = "First";
            else
                semester = "Second";

            // checks if the student has registered and if registration have been approved by his/her adviser,
            // returns true/false then assigns the returned value to registeredApproved variable
            var registeredApproved = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == student.Level && x.Semester == semester)
                .Any(x => x.Status == "Approved");

            // checks if the student has registered and if registration is pending approval
            // returns true/false then assigns the returned value to registeredPending variable
            var registeredPending = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == student.Level && x.Semester == semester)
                .Any(x => x.Status == "Pending");

            if (registeredApproved) // if approved return the View _Approved.cshtml
            {
                return PartialView("_Approved");
            }
            else if (registeredPending) //otherwise, return the View _Pending.cshtml
            {
                return PartialView("_Pending");
            }
            // when none of those conditions is satisfied it returns the RegisterCourse passing the
            // model along so the list of courses is populated
            return PartialView("RegisterCourse", model);
        }

        // handles the post back of the selected courses when the Register button is clicked
        [HttpPost]
        public ActionResult RegisterCourse(CourseSelectionViewModel form)
        {
            // gets all the list of courses in the DB and stores it in the courses variable
            var courses = Database.Session.Query<Course>().ToList();

            // same old way of retrieving a student, by now you must be tired of seeing this
            var id = Database.Session.Query<Student>()
                .Where(s => s.RegistrationNumber == User.Identity.Name)
                .Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            // gets all the id's of all selected courses
            var selectedIds = form.getSelectedIds();

            // Use the ids to retrieve the records for the selected course
            // from the database:
            var selectedCourse = from x in courses
                                 where selectedIds.Contains(x.CourseId)
                                 select x;

            // a try/catch block to handle exceptions
            try
            {
                // iterates through all selected courses
                foreach (var course in selectedCourse)
                {
                    // for each course it assigns their CourseId, StudentId, Level, Semester, Status
                    var enrollment = new Enrollment
                    {
                        CourseId = course.CourseId,
                        StudentId = student.Id,
                        Level = course.Level,
                        Semester = course.Semester,
                        Status = "Pending" // allows the course adviser to see pending approval courses
                    };
                    Database.Session.Save(enrollment); // saves each course to the DB
                }
                // calls the Success() to display a success message
                Success("Registration successful", true);
                return RedirectToAction("index"); // returns to the homepage
            }
            catch
            {
                // calls the Danger() if an exception occur
                Danger("Oops! We are sorry, an error occurred while processing your request.\nPlease try again later");
                return RedirectToAction("index");
            }
        }

        // handles the ViewRegistration.cshtml
        public ActionResult ViewRegistration()
        {
            // creates an object of WelcomeViewRegistration VM
            var model = new WelcomeViewRegistration();

            // same old
            var id = Database.Session.Query<Student>().Where(s => s.RegistrationNumber == User.Identity.Name).Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            IQueryable<EnrolledCourses> query; // a query variable

            //assigns the Student Model's properties values to their respective VM equivalent so they
            // are populated on page load
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.MiddleName = student.MiddleName;
            model.RegistrationNumber = student.RegistrationNumber;
            model.Department = student.Department;
            model.Level = student.Level;
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

            // same old semester determination but with a tiny difference
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                // a query to populate EnrolledCourses VM properties using JOINS
                // this is used to populate the courses registered by the student
                query = from students in Database.Session.Query<Student>()
                        join enrolled in Database.Session.Query<Enrollment>() on students.Id equals enrolled.StudentId
                        join courses in Database.Session.Query<Course>() on enrolled.CourseId equals courses.CourseId
                        where courses.Level == student.Level && courses.Semester == "First"
                        select new EnrolledCourses
                        {
                            Courses = courses,
                            Enrolled = enrolled
                        };
            }
            else
            {
                // same as above but for second semester
                query = from students in Database.Session.Query<Student>()
                        join enrolled in Database.Session.Query<Enrollment>() on students.Id equals enrolled.StudentId
                        join courses in Database.Session.Query<Course>() on enrolled.CourseId equals courses.CourseId
                        where courses.Level == student.Level && courses.Semester == "Second"
                        select new EnrolledCourses
                        {
                            Courses = courses,
                            Enrolled = enrolled
                        };
            }

            // assigns the query above to the WelcomeViewRegistration VM Enroll property
            model.Enroll = query.ToList();

            string semester = null;
            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
                semester = "First";
            else
                semester = "Second";

            // same as that in RegisterCourse()
            var isApproved = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == student.Level && x.Semester == semester)
                .Any(x => x.Status == "Approved");

            var isPending = Database.Session.Query<Enrollment>().Where(x => x.StudentId == student.Id && x.Level == student.Level && x.Semester == semester)
                .Any(x => x.Status == "Pending");

            if (isApproved)
            {
                // returns ApprovedRegistration.cshtml, passing the model
                return PartialView("ApprovedRegistration", model);
            }
            else if (isPending)
            {
                // returns PendingRegistration.cshtml, passing the model
                return PartialView("PendingRegistration", model);
            }

            // otherwise, returns ViewRegistration.cshtml if the conditions above are not satisfied
            return PartialView();
        }

        public ActionResult AddCourses()
        {
            List<Course> courses;
            var model = new AddCourseSelectionViewModel();

            var id = Database.Session.Query<Student>()
                .Where(s => s.RegistrationNumber == User.Identity.Name)
                .Select(s => s.Id).First();
            var student = Database.Session.Load<Student>(id);

            if (student == null)
                return HttpNotFound();

            if (DateTime.Now.Month < 3 || DateTime.Now.Month >= 10)
            {
                courses = Database.Session.Query<Course>()
                    .Where(c => c.Level <= student.Level && c.Level != student.Level && c.Semester == "First").ToList();
            }
            else
            {
                courses = Database.Session.Query<Course>()
                    .Where(c => c.Level <= student.Level && c.Level != student.Level && c.Semester == "Second").ToList();
            }

            foreach (var course in courses)
            {
                model.Courses.Add(new AddCourseViewModel
                {
                    Id = course.CourseId,
                    CourseCode = course.CourseCode,
                    CourseTitle = course.CourseTitle,
                    Credit = course.Credit,
                    Type = course.Type,
                    IsSelected = false
                });
            }

            return PartialView(model);
        }
    }
}