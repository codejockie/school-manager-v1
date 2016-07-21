using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CourseRegistrationSystem.Models
{
    // for the courses table in the DB
    public class Course
    {
        public virtual int CourseId { get; set; } // course_id column
        public virtual string CourseCode { get; set; } // course_code column
        public virtual string CourseTitle { get; set; } // course_title column
        public virtual string Department { get; set; } // department column
        public virtual string LecturerName { get; set; } // lecturer_name column
        public virtual int Level { get; set; } // level column
        public virtual string Semester { get; set; } // semester column
        public virtual string Credit { get; set; } // credit column
        public virtual string Type { get; set; } // type column
    }

    // maps the Course class' properties to their respective columns
    // NHibernate is in charge of doing the mapping
    // inherits the generic class ClassMapping, note the Course class in the angular bracket
    public class CourseMap : ClassMapping<Course>
    {
        public CourseMap()
        {
            Table("courses"); // tells NHibernate the table it wants to map to

            // maps the Id column, it must use the Id() for Id mapping
            Id(x => x.CourseId, x =>
            {
                x.Column("course_id");
                // defines how the Id column is generated, Identity (auto-increment)
                x.Generator(Generators.Identity);
            });

            // maps the CourseCode, note how it uses Property() for other properties
            Property(x => x.CourseCode, x =>
            {
                x.Column("course_code");
                x.NotNullable(true);
            });
            Property(x => x.CourseTitle, x => 
            {
                x.Column("course_title");
                x.NotNullable(true);
            });
            Property(x => x.Department, x => x.NotNullable(true));
            Property(x => x.LecturerName, x =>
            {
                x.Column("lecturer_name");
                x.NotNullable(false);
            });
            Property(x => x.Level, x => x.NotNullable(true));
            Property(x => x.Semester, x => x.NotNullable(true));
            Property(x => x.Credit, x => x.NotNullable(true));
            Property(x => x.Type, x => x.NotNullable(true));
        }
    }
}
