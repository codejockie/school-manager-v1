using System;
using System.Web.Mvc;

namespace CourseRegistrationSystem.Infrastructure
{
    // this sets where this attribute/class can be used, in our case
    // it can only be applied to classes/controllers or methods   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    // this class determines which controller is currently being displayed
    // in the admin and course adviser areas
    public class SelectedTabAttribute : ActionFilterAttribute
    {
        private readonly string _selectedTab; // a readonly field

        // constructor intialising the field
        public SelectedTabAttribute(string selectedTab)
        {
            _selectedTab = selectedTab;
        }

        // on executing a controller, it assigns that field to the ViewBag.SelectedTab variable
        // ViewBag is a dynamic property
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.SelectedTab = _selectedTab;
        }
    }
}
