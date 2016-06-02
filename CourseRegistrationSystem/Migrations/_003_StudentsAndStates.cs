using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(3, "Students and States tables")]
    public class _003_StudentsAndStates : Migration
    {
        public override void Down()
        {
            Delete.Table("students");
            Delete.Table("states");
        }

        public override void Up()
        {
            Create.Table("states")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("name").AsString(20);

            Insert.IntoTable("states").Row(new { name = "Abia" });
            Insert.IntoTable("states").Row(new { name = "Adamawa" });
            Insert.IntoTable("states").Row(new { name = "Akwa Ibom" });
            Insert.IntoTable("states").Row(new { name = "Anambra" });
            Insert.IntoTable("states").Row(new { name = "Bauchi" });
            Insert.IntoTable("states").Row(new { name = "Bayelsa" });
            Insert.IntoTable("states").Row(new { name = "Benue" });
            Insert.IntoTable("states").Row(new { name = "Bornu" });
            Insert.IntoTable("states").Row(new { name = "Cross River" });
            Insert.IntoTable("states").Row(new { name = "Delta" });
            Insert.IntoTable("states").Row(new { name = "Ebonyi" });
            Insert.IntoTable("states").Row(new { name = "Edo" });
            Insert.IntoTable("states").Row(new { name = "Ekiti" });
            Insert.IntoTable("states").Row(new { name = "Enugu" });
            Insert.IntoTable("states").Row(new { name = "FCT Abuja" });
            Insert.IntoTable("states").Row(new { name = "Gombe" });
            Insert.IntoTable("states").Row(new { name = "Imo" });
            Insert.IntoTable("states").Row(new { name = "Jigawa" });
            Insert.IntoTable("states").Row(new { name = "Kaduna" });
            Insert.IntoTable("states").Row(new { name = "Kano" });
            Insert.IntoTable("states").Row(new { name = "Katsina" });
            Insert.IntoTable("states").Row(new { name = "Kebbi" });
            Insert.IntoTable("states").Row(new { name = "Kogi" });
            Insert.IntoTable("states").Row(new { name = "Kwara" });
            Insert.IntoTable("states").Row(new { name = "Lagos" });
            Insert.IntoTable("states").Row(new { name = "Nasarawa" });
            Insert.IntoTable("states").Row(new { name = "Niger" });
            Insert.IntoTable("states").Row(new { name = "Ogun" });
            Insert.IntoTable("states").Row(new { name = "Ondo" });
            Insert.IntoTable("states").Row(new { name = "Osun" });
            Insert.IntoTable("states").Row(new { name = "Oyo" });
            Insert.IntoTable("states").Row(new { name = "Plateau" });
            Insert.IntoTable("states").Row(new { name = "Rivers" });
            Insert.IntoTable("states").Row(new { name = "Sokoto" });
            Insert.IntoTable("states").Row(new { name = "Taraba" });
            Insert.IntoTable("states").Row(new { name = "Yobe" });
            Insert.IntoTable("states").Row(new { name = "Zamfara" });

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
                .WithColumn("state").AsInt32().ForeignKey("states", "id").OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("lga").AsString(50).NotNullable()
                .WithColumn("hometown").AsString(128).NotNullable()
                .WithColumn("nationality").AsString(50).NotNullable()
                .WithColumn("course_of_study").AsString(128).NotNullable()
                .WithColumn("department").AsString(128).NotNullable()
                .WithColumn("level").AsString(3).NotNullable()
                .WithColumn("blood_group").AsString(3).Nullable()
                .WithColumn("genotype").AsString(3).Nullable()
                .WithColumn("disability").AsString(20).Nullable()
                .WithColumn("photo").AsBinary().Nullable();

            Insert.IntoTable("students").Row(new { firstname = "Sandra", lastname = "Ozokolo", middlename = "Chikaodinaka", registration_number = "2012/182093", email = "ozokolosandra@gmail.com", address = "10 Just Any Other Place", phone_number = "2348100080078", date_of_birth = DateTime.UtcNow.Date, gender = "Female", state = 14, lga = "Ezeagu", hometown = "Ezeagu", nationality = "Nigerian", course_of_study = "Computer Science", department = "Computer Science", level = "400", blood_group = "A-", genotype = "AA", disability = "none" });
            Insert.IntoTable("students").Row(new { firstname = "Kennedy", lastname = "John", middlename = "Fitzgerald", registration_number = "2012/000010", email = "jfk@jfk.com", address = "10 Just Any Other Place", phone_number = "+1452278861447", date_of_birth = DateTime.UtcNow.Date, gender = "Male", state = 17, lga = "Owerri", hometown = "Owerri", nationality = "Nigerian", course_of_study = "Computer Science", department = "Computer Science", level = "400", blood_group = "A-", genotype = "AA", disability = "none" });
        }
    }
}
