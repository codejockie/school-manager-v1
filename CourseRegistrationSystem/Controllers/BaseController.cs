using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseRegistrationSystem.Helpers;

namespace CourseRegistrationSystem.Controllers
{
    // This is the BaseController class, all classes in the Welcome Controller extend/inherit it
    // It's basically for the showing of error/success messages to the user
    public class BaseController : Controller
    {
        // Success() method displays success messages/alert
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        // Information() method displays info messages/alert
        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        // Warning() method shows warning alert
        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        // Danger() method shows error message/alert
        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        // the AddAlert() is called by all aforementioned methods above
        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            // checks if TempData Dict. contains a specified key
            // defined in the Alerts.cs in Helpers folder
            // if true it casts the key's value to a List<Alert> and assigns
            // it to alerts variable, otherwise it creates a new List of type Alert
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            // here it populates the properties defined in the Alert class
            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            // assigns the alerts variable to the TempData Dictionary
            TempData[Alert.TempDataKey] = alerts;
        }
    }
}