using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.Infrastructure
{
    // just like the FileSizeAttribute this just allow you to define the image type
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types; // a List of string field

        // constructor initialising the field
        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        // checks for validity of the image extension/format
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        // specifies the error message to display
        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", string.Join(", ", _types));
        }
    }
}