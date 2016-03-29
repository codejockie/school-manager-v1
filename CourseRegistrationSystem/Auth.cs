using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem
{
    public static class Auth
    {
        private const string _userKey = "CourseRegistrationSystem.Auth.Userkey";

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[_userKey] as User;
                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[_userKey] = user;
                }

                return user;
            }
        }
    }
}
