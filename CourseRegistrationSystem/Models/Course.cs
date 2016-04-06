using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace CourseRegistrationSystem.Models
{
    public class Course
    {
        public virtual int CourseId { get; set; }
        public virtual string CourseCode { get; set; }
        public virtual string CourseTitle { get; set; }
        public virtual string LecturerName { get; set; }
        public virtual string Level { get; set; }
        public virtual string Semester { get; set; }
        public virtual string Credit { get; set; }
        public virtual string Type { get; set; }
    }

    public class CourseMap : ClassMapping<Course>
    {
        public CourseMap()
        {
            Table("courses");

            Id(x => x.CourseId, x =>
            {
                x.Column("course_id");
                x.Generator(Generators.HighLow, g => g.Params(new { max_lo = 100, table = "students_entity_unique_key" }));
            });

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
            Property(x => x.LecturerName, x =>
            {
                x.Column("lecturer_name");
                x.NotNullable(true);
            });
            Property(x => x.Level, x => x.NotNullable(true));
            Property(x => x.Semester, x => x.NotNullable(true));
            Property(x => x.Credit, x => x.NotNullable(true));
            Property(x => x.Type, x => x.NotNullable(true));
        }
    }
}
