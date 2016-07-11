using CourseRegistrationSystem.Infrastructure;
using CourseRegistrationSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseRegistrationSystem.ViewModels
{
    public enum States
    {
        Abia,
        Adamawa,
        [Display(Name = "Akwa Ibom")]
        Akwa_Ibom,
        Anambra,
        Bauchi,
        Bayelsa,
        Benue,
        Bornu,
        [Display(Name = "Cross River")]
        Cross_River,
        Delta,
        Ebonyi,
        Edo,
        Ekiti,
        Enugu,
        [Display(Name = "FCT Abuja")]
        FCT_Abuja,
        Gombe,
        Imo,
        Jigawa,
        Kaduna,
        Kano,
        Katsina,
        Kebbi,
        Kogi,
        Kwara,
        Lagos,
        Nasarawa,
        Niger,
        Ogun,
        Ondo,
        Osun,
        Oyo,
        Plateau,
        Rivers,
        Sokoto,
        Taraba,
        Yobe,
        Zamfara
    }
    public class WelcomeIndex
    {
        public string FirstName { get; set; }
        public byte[] Photo { get; set; }

        [Required]
        [FileSize(150000)]
        [FileTypes("jpg,jpeg")]
        public HttpPostedFileBase ProfilePic { get; set; }
    }
    public class WelcomeBiodata
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

        [Required, DisplayName("Date of Birth")]
        public string DateOfBirth { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        public States State { get; set; }

        [Required, MaxLength(50), Display(Name = "L. G. A.")]
        public string LGA { get; set; }

        [Required, MaxLength(128)]
        public string Hometown { get; set; }

        [Required, EnumDataType(typeof(Country))]
        public Country Country { get; set; }

        [Required, DisplayName("Course"), MaxLength(128)]
        public string CourseOfStudy { get; set; }

        [Required, MaxLength(50)]
        public string Department { get; set; }

        [Required, EnumDataType(typeof(Level))]
        public Level Level { get; set; }

        [DisplayName("Blood Group"), EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required, EnumDataType(typeof(Genotype))]
        public Genotype Genotype { get; set; }

        [EnumDataType(typeof(Disability))]
        public Disability Disability { get; set; }

        [Required, Display(Name = "Sponsor's Name")]
        public string SponsorName { get; set; }

        [Required, Display(Name = "Sponsor's Phone")]
        public string SponsorPhone { get; set; }
    }

    public class WelcomeChangePassword
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DisplayName("Confirm Password")]
        [DataType(DataType.Password), System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password Must Match")]
        public string ConfirmPassword { get; set; }
    }

    public class WelcomeViewRegistration
    {
        [Required, DisplayName("Firstname"), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, DisplayName("Lastname"), MaxLength(50)]
        public string LastName { get; set; }

        [DisplayName("Middlename"), MaxLength(50)]
        public string MiddleName { get; set; }

        [Required, DisplayName("Matric No"), MaxLength(11)]
        public string RegistrationNumber { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(128)]
        public string Email { get; set; }

        [Required, DataType(DataType.MultilineText), MaxLength(256)]
        public string Address { get; set; }

        [Required, DisplayName("Phone"), DataType(DataType.PhoneNumber), MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required, DisplayName("Date of Birth"), DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public string Gender { get; set; }

        [Required, Display(Name = "Sponsor")]
        public string SponsorName { get; set; }

        [Required, Display(Name = "Sponsor Phone")]
        public string SponsorPhone { get; set; }

        [Required]
        public string State { get; set; }

        [Required, MaxLength(50), Display(Name = "L. G. A.")]
        public string LGA { get; set; }

        [Required, MaxLength(128)]
        public string Hometown { get; set; }

        [Required, DisplayName("Course"), MaxLength(128)]
        public string CourseOfStudy { get; set; }

        [Required, MaxLength(50)]
        public string Department { get; set; }

        [Required, EnumDataType(typeof(Level))]
        public string Level { get; set; }

        [Required]
        [FileSize(150000)]
        [FileTypes("jpg,jpeg")]
        public byte[] Photo { get; set; }

        public string StudentType { get; set; }

        public List<EnrolledCourses> Enroll { get; set; }
    }

    public class EnrolledCourses
    {
        public Course Courses { get; set; }
        public Enrollment Enrolled { get; set; }
        public string Session
        {
            get
            {
                if (DateTime.Now.Month >= 10)
                {
                    return DateTime.Now.Year + "/" + DateTime.Now.Year + 1;
                }
                else
                {
                    return DateTime.Now.Year - 1 + "/" + DateTime.Now.Year;
                }
            }
        }
        public string Submitted { get; set; }
        public string Approved { get; set; }
    }
}
