using System.Web.Mvc;

namespace CourseRegistrationSystem.Areas.CourseAdviser
{
    public class CourseAdviserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "courseadviser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CourseAdviser_default",
                "courseadviser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}