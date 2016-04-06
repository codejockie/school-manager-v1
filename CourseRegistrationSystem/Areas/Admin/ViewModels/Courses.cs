using CourseRegistrationSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistrationSystem.Areas.Admin.ViewModels
{
    public class CoursesIndex
    {
        public IEnumerable<Course> Courses { get; set; }
    }

    public class CoursesNew
    {
        [Required, MaxLength(7), DisplayName("Course Code")]
        public string CourseCode { get; set; }

        [Required, MaxLength(150), DisplayName("Course Title")]
        public string CourseTitle { get; set; }

        [Required, MaxLength(128), DisplayName("Lecturer")]
        public string LecturerName { get; set; }

        [Required, MaxLength(10)]
        public string Level { get; set; }

        [Required, MaxLength(7)]
        public string Semester { get; set; }

        [Required, MaxLength(7)]
        public string Credit { get; set; }

        [Required, MaxLength(10)]
        public string Type { get; set; }
    }

    public class CoursesEdit
    {
        [Required, MaxLength(7), DisplayName("Course Code")]
        public string CourseCode { get; set; }

        [Required, MaxLength(150), DisplayName("Course Title")]
        public string CourseTitle { get; set; }

        [Required, MaxLength(128), DisplayName("Lecturer")]
        public string LecturerName { get; set; }

        [Required, MaxLength(10)]
        public string Level { get; set; }

        [Required, MaxLength(7)]
        public string Semester { get; set; }

        [Required, MaxLength(7)]
        public string Credit { get; set; }

        [Required, MaxLength(10)]
        public string Type { get; set; }
    }
}
