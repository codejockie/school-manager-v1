using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace CourseRegistrationSystem.Models
{
    public class Student
    {
        public virtual int StudentId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string RegNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Gender { get; set; }
        public virtual int StateId { get; set; }
        public virtual string LGA { get; set; }
        public virtual string Nationality { get; set; }
        public virtual string CourseOfStudy { get; set; }
        public virtual string Department { get; set; }
        public virtual int Level { get; set; }
        public virtual string BloodGroup { get; set; }
        public virtual string Genotype { get; set; }
        public virtual string Disability { get; set; }
        public virtual byte[] Photo { get; set; }
    }

    public class StudentMap : ClassMapping<Student>
    {
        public StudentMap()
        {
            Table("students");

            Id(x => x.StudentId, x =>
            {
                x.Column("student_id");
                x.Generator(Generators.HighLow, g => g.Params(new { max_lo = 50, table = "courses_entity_unique_key" }));
            });

            Property(x => x.FirstName, x => x.NotNullable(true));
            Property(x => x.LastName, x => x.NotNullable(true));
            Property(x => x.MiddleName);
            Property(x => x.RegNumber, x =>
            {
                x.Column("reg_number");
                x.NotNullable(true);
            });
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.Address, x => x.NotNullable(true));
            Property(x => x.PhoneNumber, x =>
            {
                x.Column("phone_number");
                x.NotNullable(true);
            });
            Property(x => x.DateOfBirth, x =>
            {
                x.Column("date_of_birth");
                x.NotNullable(true);
            });
            Property(x => x.Gender, x => x.NotNullable(true));
            Property(x => x.StateId, x =>
            {
                x.Column("state_id");
                x.NotNullable(true);
            });
            Property(x => x.LGA, x => x.NotNullable(true));
            Property(x => x.Nationality, x => x.NotNullable(true));
            Property(x => x.CourseOfStudy, x =>
            {
                x.Column("course_of_study");
                x.NotNullable(true);
            });
            Property(x => x.Department, x => x.NotNullable(true));
            Property(x => x.Level, x => x.NotNullable(true));
            Property(x => x.BloodGroup, x =>
            {
                x.Column("blood_group");
                x.NotNullable(false);
            });
            Property(x => x.Genotype, x => x.NotNullable(false));
            Property(x => x.Disability, x => x.NotNullable(false));
            Property(x => x.Photo, x => x.NotNullable(false));
        }
    }
}
