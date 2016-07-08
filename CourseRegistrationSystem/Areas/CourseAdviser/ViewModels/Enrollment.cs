using CourseRegistrationSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.Areas.CourseAdviser.ViewModels
{
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