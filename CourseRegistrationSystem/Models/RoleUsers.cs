using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.Models
{
    public class RoleUsers
    {
        public virtual int UserId { get; set; }
        public virtual int RoleId { get; set; }
    }

    public class RoleUsersMap : ClassMapping<RoleUsers>
    {
        public RoleUsersMap()
        {
            Table("role_users");

            Property(x => x.UserId, x =>
            {
                x.NotNullable(true);
                x.Column("user_id");
            });
            Property(x => x.RoleId, x =>
            {
                x.NotNullable(true);
                x.Column("role_id");
            });
        }
    }
}