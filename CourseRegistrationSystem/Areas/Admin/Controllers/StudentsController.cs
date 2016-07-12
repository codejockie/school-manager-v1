using CourseRegistrationSystem.Areas.Admin.ViewModels;
using CourseRegistrationSystem.Controllers;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.Admin.Controllers
{
    [SelectedTab("students")]
    public class StudentsController : BaseController
    {
        // GET: Admin/Students
        public ActionResult Index()
        {
            return View(new StudentsIndex
            {
                Students = Database.Session.Query<Student>().ToList()
            });
        }

        [SelectedTab("register")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(StudentsNew form)
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

            return RedirectToAction("newstudent", "students");
        }

        public ActionResult NewStudent()
        {
            StudentsNewStudent newStudent = null;

            if (TempData["matricno"] != null && TempData["email"] != null)
            {
                newStudent = new StudentsNewStudent
                {
                    Username = TempData["matricno"].ToString(),
                    Email = TempData["email"].ToString()
                };
            }

            return View(newStudent);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewStudent(StudentsNewStudent form)
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
                return RedirectToAction("index", "students");
            }

            return RedirectToAction("index", "students");
        }
    }
}