using FluentMigrator;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(2, "Courses Table")]
    public class _002_Courses : Migration
    {
        public override void Down()
        {
            Delete.Table("courses");
        }

        public override void Up()
        {
            Create.Table("courses")
                .WithColumn("course_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("course_code").AsString(7).NotNullable()
                .WithColumn("course_title").AsString(150).NotNullable()
                .WithColumn("lecturer_name").AsString(128).Nullable().WithDefaultValue("Not Available")
                .WithColumn("level").AsString(10).NotNullable()
                .WithColumn("semester").AsString(7).NotNullable()
                .WithColumn("credit").AsString(7).NotNullable()
                .WithColumn("type").AsString(10).NotNullable();

            Insert.IntoTable("courses").Row(new { course_code = "CS101", course_title = "Introduction to Computer Science", lecturer_name = "C. I. Nwoye", level = "100", semester = "First", credit = "2", type = "Major" });
        }
    }
}
