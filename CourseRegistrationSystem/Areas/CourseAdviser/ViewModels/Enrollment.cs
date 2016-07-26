using CourseRegistrationSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.Areas.CourseAdviser.ViewModels
{
    public class CoursesIndex
    {
        public IEnumerable<Course> Courses { get; set; }
    }

    public class CoursesAddInfo
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(Name = "Course Advice")]
        public string CourseInfo { get; set; }
    }
    public class StudentsIndex
    {
        public IEnumerable<Student> Students { get; set; }
    }

    public class StudentsViewCourses
    {
        public Course Courses { get; set; }
        public Student Students { get; set; }
        public Enrollment Status { get; set; }
    }
}