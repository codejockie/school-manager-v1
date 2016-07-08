using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.Models
{
    public class Enrollment
    {
        public virtual int Id { get; set; }
        public virtual int CourseId { get; set; }
        public virtual int StudentId { get; set; }
        public virtual string Level { get; set; }
        public virtual string Semester { get; set; }
        public virtual string Status { get; set; }
    }

    public class EnrollmentMap : ClassMapping<Enrollment>
    {
        public EnrollmentMap()
        {
            Table("enrollment");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(c => c.CourseId, c => 
            {
                c.Column("course_id");
                c.NotNullable(true);
            });
            Property(s => s.StudentId, s =>
            {
                s.Column("student_id");
                s.NotNullable(true);
            });
            Property(l => l.Level, l => l.NotNullable(true));
            Property(s => s.Semester, s => s.NotNullable(true));
            Property(s => s.Status, s => s.NotNullable(true));
        }
    }
}