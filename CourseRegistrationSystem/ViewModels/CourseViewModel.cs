using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.ViewModels
{
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
        public string Level { get; set; }
        public string StudentType { get; set; }
        public List<CourseViewModel> Courses { get; set; }

        public CourseSelectionViewModel()
        {
            Courses = new List<CourseViewModel>();
        }

        public IEnumerable<int> getSelectedIds()
        {
            var selected = Courses.Where(c => c.IsSelected).Select(x => x.Id).ToList();
            return selected;
        }
    }

    public class CourseViewModel
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string Credit { get; set; }
        public string Type { get; set; }
    }
}