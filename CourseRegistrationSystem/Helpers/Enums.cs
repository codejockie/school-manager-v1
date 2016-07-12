using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationSystem.Helpers
{
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