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
    [Authorize(Roles = "admin")]
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

        public ActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        // handles the post when add student form is submitted
        // StudentsNew VM is passed as a parameter
        public ActionResult New(StudentsNew form)
        {
            // creates a student model object/instance
            var student = new Student();

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            // assign all typed in values in the various form fields
            // to their respective Model properties
            student.FirstName = form.FirstName;
            student.LastName = form.LastName;
            student.MiddleName = form.MiddleName;
            student.Address = form.Address;
            student.RegistrationNumber = form.RegistrationNumber;
            student.PhoneNumber = form.PhoneNumber;
            student.Email = form.Email;
            student.DateOfBirth = DateTime.Parse(form.DateOfBirth);
            student.Gender = form.Gender.ToString();
            student.SponsorName = form.SponsorName;
            student.SponsorPhone = form.SponsorPhone;
            student.Country = form.Country.ToString();
            student.State = form.State.ToString();
            student.LGA = form.LGA;
            student.Hometown = form.Hometown;

            // checks which Level enum was selected on the form
            // then assigns differing levels to the student model
            // based on the selection
            switch (form.Level)
            {
                case Helpers.Level.FirstYear:
                    student.Level = 100; break;
                case Helpers.Level.SecondYear:
                    student.Level = 200; break;
                case Helpers.Level.ThirdYear:
                    student.Level = 300; break;
                case Helpers.Level.FourthYear:
                    student.Level = 400; break;
                case Helpers.Level.FifthYear:
                    student.Level = 500; break;
            }

            student.CourseOfStudy = form.CourseOfStudy;
            student.Department = form.Department;
            student.Disability = form.Disability.ToString();
            student.BloodGroup = form.BloodGroup.ToString();
            student.Genotype = form.Genotype.ToString();
            student.StudentType = form.StudentType.ToString();

            // checks if a picture was upload, if true it
            // enters the block else it skips the block
            if (form.Photo != null)
            {
                // converts the pix to byte & assigns it to an array (uploadedPhoto)
                byte[] uploadedPhoto = new byte[form.Photo.InputStream.Length];
                // reads the byte array
                form.Photo.InputStream.Read(uploadedPhoto, 0, uploadedPhoto.Length);
                // assigns the converted pix to the student Photo property
                student.Photo = uploadedPhoto; 
            }

            // calls Save()
            Database.Session.Save(student);

            return RedirectToAction("index", "students");
        }
    }
}