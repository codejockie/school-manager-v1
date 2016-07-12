using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationSystem.ViewModels
{
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
}
