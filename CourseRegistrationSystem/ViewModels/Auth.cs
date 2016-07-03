using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationSystem.ViewModels
{
    // ViewModels are used for passing data to Views via the Controller
    // VMs use this convention, ControllerName/ActionMethodName
    // VMs are sub-Models ie. they contain almost same properties as Models
    // these VMs are for the student login and admin/adviser login View respectively
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
