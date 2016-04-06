using FluentMigrator;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(2, "Courses Table")]
    public class _002_Courses : Migration
    {
        public override void Down()
        {
            Delete.Table("courses_entity_unique_key");
            Delete.Table("courses");
        }

        public override void Up()
        {
            Create.Table("courses")
                .WithColumn("course_id").AsInt32().PrimaryKey()
                .WithColumn("course_code").AsString(7).NotNullable()
                .WithColumn("course_title").AsString(150).NotNullable()
                .WithColumn("lecturer_name").AsString(128).NotNullable()
                .WithColumn("level").AsString(10).NotNullable()
                .WithColumn("semester").AsString(7).NotNullable()
                .WithColumn("credit").AsString(7).NotNullable()
                .WithColumn("type").AsString(10).NotNullable();

            Create.Table("courses_entity_unique_key")
                .WithColumn("next_hi").AsInt32();

            Insert.IntoTable("courses_entity_unique_key").Row(new { next_hi = 10 });
        }
    }
}
