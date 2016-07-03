using FluentMigrator;
using System.Data;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(1)] // the number is used to keep track of the migration
    // this class is used to migrate/create the users, roles, role_users tables
    /// dynamically when DeployDbb-Dev.bat in Tools folder is executed
    public class _001_UsersAndRoles : Migration
    {
        // called when rolling back the migration, deletes the specified tables
        public override void Down()
        {
            Delete.Table("role_users");
            Delete.Table("roles");
            Delete.Table("users");
        }

        // called to migrate the specified tables
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().Identity().PrimaryKey() // PK Identity(auto-increment)
                .WithColumn("username").AsString(128)
                .WithColumn("email").AsCustom("VARCHAR(256)")
                .WithColumn("password_hash").AsString(128);

            Create.Table("roles")
                .WithColumn("id").AsInt32().Identity().PrimaryKey() // PK Identity(auto-increment)
                .WithColumn("name").AsString(128);

            // creates a table with two FKs from users and roles tables
            Create.Table("role_users")
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("role_id").AsInt32().ForeignKey("roles", "id").OnDelete(Rule.Cascade);
        }
    }
}
