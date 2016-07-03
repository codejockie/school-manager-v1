using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CourseRegistrationSystem.Infrastructure
{
    // this class is used to restrict the size of image that is uploaded in the profile
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize; // a readonly field

        // constructor that initialises the field
        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        // checks for validity of the image's size
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return (value as HttpPostedFileBase).ContentLength <= _maxSize;
        }

        // formats the error message to display if the size is not valid
        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0}", _maxSize);
        }
    }
}