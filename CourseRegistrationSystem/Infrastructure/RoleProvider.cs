using CourseRegistrationSystem.Models;
using NHibernate.Linq;
using System;
using System.Linq;

namespace CourseRegistrationSystem.Infrastructure
{
    // used to implement the authorisation/authentication of the system
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        // returns an arrray of roles a particular username belongs
        public override string[] GetRolesForUser(string username)
        {
            return Auth.User.Roles.Select(role => role.Name).ToArray();
        }

        // determines if a username belongs to a specified role
        public override bool IsUserInRole(string username, string roleName)
        {
            var role = from u in Database.Session.Query<User>()
                       from ru in Database.Session.Query<RoleUsers>()
                       from r in Database.Session.Query<Role>()
                       where u.Id == ru.UserId && r.Id == ru.RoleId && u.Username == username
                       select r.Name;

            return role.Any(r => r.Equals(roleName));
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
