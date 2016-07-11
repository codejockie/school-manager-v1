using FluentMigrator;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(5, "Messages table")]
    public class _005_Messages : Migration
    {
        public override void Down()
        {
            Delete.Table("messages");
        }

        public override void Up()
        {
            Create.Table("messages")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("message").AsCustom("TEXT").NotNullable()
                .WithColumn("student_id").AsInt32().ForeignKey("students", "id").OnDelete(System.Data.Rule.Cascade).Nullable()
                .WithColumn("adviser_id").AsInt32().ForeignKey("users", "id").OnDelete(System.Data.Rule.Cascade).Nullable()
                .WithColumn("status").AsString(10).NotNullable()
                .WithColumn("created_at").AsTime().NotNullable();
        }
    }
}