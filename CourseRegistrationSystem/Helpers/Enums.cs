using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationSystem.Helpers
{
    // these are simply enums used for the dropdownlists in the student profile details
    public enum Gender
    {
        Female,
        Male
    }
    public enum Disability
    {

        None,
        Blind,
        Crippled,
        Deaf,
        Dumb
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

    public enum Level : short
    {
        [Display(Name = "100 Level")]
        FirstYear = 100,
        [Display(Name = "200 Level")]
        SecondYear = 200,
        [Display(Name = "300 Level")]
        ThirdYear = 300,
        [Display(Name = "400 Level")]
        FourthYear = 400,
        [Display(Name = "500 Level")]
        FifthYear = 500
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
        [Display(Name = "United States")]
        USA,
        Vietnam,
        Yemen,
        Zimbabwe
    }
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
}