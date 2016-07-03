using CourseRegistrationSystem.Helpers;
using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CourseRegistrationSystem.Areas.Admin.ViewModels
{
    public class StudentsIndex
    {
        public IEnumerable<Student> Students { get; set; }
    }

    public class StudentsNew
    {
        [Required, DisplayName("Firstname"), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, DisplayName("Lastname"), MaxLength(50)]
        public string LastName { get; set; }

        [DisplayName("Middlename"), MaxLength(50)]
        public string MiddleName { get; set; }

        [Required, DisplayName("Matric Number"), MaxLength(11)]
        public string RegistrationNumber { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(128)]
        public string Email { get; set; }

        [Required, DataType(DataType.MultilineText), MaxLength(256)]
        public string Address { get; set; }

        [Required, DisplayName("Phone"), DataType(DataType.PhoneNumber), MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required, DisplayName("Date of Birth"), DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required, Display(Name = "Sponsor's Name")]
        public string SponsorName { get; set; }

        [Required, Display(Name = "Sponsor's Phone")]
        public string SponsorPhone { get; set; }

        [Required]
        public States State { get; set; }

        [Required, MaxLength(50), Display(Name = "L. G. A.")]
        public string LGA { get; set; }

        [Required, MaxLength(128)]
        public string Hometown { get; set; }

        [Required, Display(Name = "Country")]
        public Country Country { get; set; }

        [Required, DisplayName("Course"), MaxLength(128)]
        public string CourseOfStudy { get; set; }

        [Required, MaxLength(50)]
        public string Department { get; set; }

        [Required]
        public Level Level { get; set; }

        [DisplayName("Blood Group")]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        public Genotype Genotype { get; set; }

        public Disability Disability { get; set; }

        [FileSize(150000)]
        [FileTypes("jpg,jpeg")]
        public HttpPostedFileBase Photo { get; set; }

        [Required, DisplayName("Student Type")]
        public StudentType StudentType { get; set; }
    }
}