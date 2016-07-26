using System.Collections.Generic;
using System.Linq;

namespace CourseRegistrationSystem.ViewModels
{
    // VMs for the course registration View
    // Each property represent what can be seen on the View, say when a student wants to register
    // on the page presented you'd see the student's firstname, reg no, level etc
    public class CourseSelectionViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName + " " + MiddleName;
            }
        }
        public string RegistrationNumber { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
        public string StudentType { get; set; }
        public List<CourseViewModel> Courses { get; set; } // List of CourseViewModel property

        public CourseSelectionViewModel()
        {
            Courses = new List<CourseViewModel>();
        }

        // method that is called in the post version of RegisterCourse() in the WelcomeController
        // it returns an enumerable/list of int of the selected/checked courses id
        public IEnumerable<int> getSelectedIds()
        {
            var selected = Courses.Where(c => c.IsSelected).Select(x => x.Id).ToList();
            return selected;
        }
    }

    // same as above except it doesn't contain the other properties like FirstName etc
    // Use to present the View for Add/Borrow courses
    public class AddCourseSelectionViewModel
    {
        public List<AddCourseViewModel> Courses { get; set; }

        public AddCourseSelectionViewModel()
        {
            Courses = new List<AddCourseViewModel>();
        }

        public IEnumerable<int> getSelectedIds()
        {
            var selected = Courses.Where(c => c.IsSelected).Select(x => x.Id).ToList();
            return selected;
        }
    }

    // presents the courses shown when Register Courses is clicked in the portal
    public class CourseViewModel
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string CourseInfo { get; set; }
        public string Credit { get; set; }
        public string Type { get; set; }
    }

    // same as above but for Add/Borrow courses
    public class AddCourseViewModel
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string Credit { get; set; }
        public string Type { get; set; }
    }
}