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

namespace CourseRegistrationSystem.ViewModels
{

    public enum Gender
    {
        Female,
        Male
    }
    public enum Disability
    {
        Blind,
        Crippled,
        Deaf,
        Dumb,
        None
    }

    public enum Genotype
    {
        AA,
        AS,
        SS
    }

    public enum StudentType
    {
        Undergraduate,
        Postgraduate
    }

    public enum Level
    {
        [Display(Name = "100 Level")]
        LevelOne,
        [Display(Name = "200 Level")]
        LevelTwo,
        [Display(Name = "300 Level")]
        LevelThree,
        [Display(Name = "400 Level")]
        LevelFour,
        [Display(Name = "500 Level")]
        LevelFive
    }

    public enum BloodGroup
    {
        [Display(Name = "A-")]
        ANegative,
        [Display(Name = "A+")]
        APositive,
        [Display(Name = "AB-")]
        ABNegative,
        [Display(Name = "AB+")]
        ABPositive,
        [Display(Name = "B-")]
        BNegative,
        [Display(Name = "B+")]
        BPositive,
        [Display(Name = "O-")]
        ONegative,
        [Display(Name = "O+")]
        OPositive
    }

    public enum Country
    {
        Argentina,
        Brazil,
        Canada,
        Denmark,
        Egypt,
        France,
        Germany,
        Ghana,
        Haiti,
        India,
        Japan,
        Korea,
        Luxembourg,
        Morocco,
        Nigeria,
        Oman,
        Portugal,
        Qatar,
        Russia,
        Spain,
        Tunisia,
        [Display(Name = "United Kingdom")]
        UnitedKingdom,
        [Display(Name = "United States of America")]
        USA,
        Vietnam,
        Yemen,
        Zimbabwe
    }
    public class AuthLogin
    {
        [Required, Display(Name = "Matric Number")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AuthAdminLogin
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AuthNewStudent
    {
        [Required, Display(Name = "Matric Number"), MaxLength(11)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DisplayName("Confirm Password")]
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Password and Confirm Password Must Match")]
        public string ConfirmPassword { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class AuthRegister
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

        [Required, EnumDataType(typeof(Gender))]
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

        [Required, EnumDataType(typeof(Country)), Display(Name = "Country")]
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

        [Required]
        [FileSize(150000)]
        [FileTypes("jpg,jpeg")]
        public HttpPostedFileBase Photo { get; set; }

        [Required, EnumDataType(typeof(StudentType)), DisplayName("Student Type")]
        public StudentType StudentType { get; set; }
    }
}
