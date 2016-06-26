using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(3, "Students table")]
    public class _003_Students : Migration
    {
        public override void Down()
        {
            Delete.Table("students");
        }

        public override void Up()
        {
            Create.Table("students")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("firstname").AsString(50).NotNullable()
                .WithColumn("lastname").AsString(50).NotNullable()
                .WithColumn("middlename").AsString(50).Nullable()
                .WithColumn("registration_number").AsString(20).NotNullable().Unique()
                .WithColumn("email").AsString(128).NotNullable()
                .WithColumn("address").AsString(256).NotNullable()
                .WithColumn("phone_number").AsString(20).NotNullable()
                .WithColumn("date_of_birth").AsDate().NotNullable()
                .WithColumn("gender").AsString(6).NotNullable()
                .WithColumn("state").AsString(20)
                .WithColumn("lga").AsString(50).NotNullable()
                .WithColumn("hometown").AsString(128).NotNullable()
                .WithColumn("country").AsString(50).NotNullable()
                .WithColumn("course_of_study").AsString(128).NotNullable()
                .WithColumn("department").AsString(128).NotNullable()
                .WithColumn("level").AsString(10).NotNullable()
                .WithColumn("blood_group").AsString(10).Nullable()
                .WithColumn("genotype").AsString(3).Nullable()
                .WithColumn("disability").AsString(10).Nullable()
                .WithColumn("sponsor_name").AsString(150).NotNullable()
                .WithColumn("sponsor_phone").AsString(20).NotNullable()
                .WithColumn("photo").AsBinary().Nullable()
                .WithColumn("student_type").AsString(15).NotNullable();

            Insert.IntoTable("students").Row(new { firstname = "Sandra", lastname = "Ozokolo", middlename = "Chikaodinaka", registration_number = "2012/182093", email = "ozokolosandra@gmail.com", address = "10 Just Any Other Place", phone_number = "+2348100080078", date_of_birth = DateTime.UtcNow.Date, gender = "Female", state = "Enugu", lga = "Ezeagu", hometown = "Ezeagu", country = "Nigeria", course_of_study = "Computer Science", department = "Computer Science", level = "LevelFour", blood_group = "APositive", genotype = "AA", disability = "None", student_type = "Undergraduate", sponsor_name = "Peter Ozokolo", sponsor_phone = "+2348020000000" });
            Insert.IntoTable("students").Row(new { firstname = "Joy", lastname = "Ali", middlename = "Chisom", registration_number = "2012/187803", email = "alijoy@joy.com", address = "10 Just Any Other Place", phone_number = "+2348020000000", date_of_birth = DateTime.UtcNow.Date, gender = "Female", state = "Enugu", lga = "Enugu-Ezike", hometown = "Enugu-Ezike", country = "Nigeria", course_of_study = "Computer Science", department = "Computer Science", level = "LevelThree", blood_group = "APositive", genotype = "AA", disability = "None", student_type = "Undergraduate", sponsor_name = "Ali", sponsor_phone = "+2348020000000" });
        }
    }
}
