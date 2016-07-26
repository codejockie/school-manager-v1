using FluentMigrator;
using System.Data;

namespace CourseRegistrationSystem.Migrations
{
    [Migration(1, "roles, users and role_users tables")] // the number is used to keep track of the migration
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
                .WithColumn("username").AsString(50)
                .WithColumn("email").AsString(50)
                .WithColumn("password_hash").AsString(128);

            Create.Table("roles")
                .WithColumn("id").AsInt32().Identity().PrimaryKey() // PK Identity(auto-increment)
                .WithColumn("name").AsString(25);

            // creates a table with two FKs from users and roles tables
            Create.Table("role_users")
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("role_id").AsInt32().ForeignKey("roles", "id").OnDelete(Rule.Cascade);

            // inserts four different users to the users table
            Insert.IntoTable("users").Row(new { username = "sandraozokolo", email = "ozokolosandra@gmail.com", password_hash = "$2a$13$PhxJKQ9c9EWFS4DH14QZYujI0SIUWYXvhAT0tUhaIq5LMcC348J.m" });
            Insert.IntoTable("users").Row(new { username = "sandra", email = "ozokolosandra@gmail.com", password_hash = "$2a$13$VDCJ.q3YRuxD1fiwsoJBV.W.X9YTQ6XpNyCw2sC7URrXBcQW7DD8S" });
            Insert.IntoTable("users").Row(new { username = "2012/182093", email = "ozokolosandra@gmail.com", password_hash = "$2a$13$utDhzUdBJq.XYxuhlFYEs.snHs6FGJaPgj6suYrHr6Bm31zcnSVDy" });
            Insert.IntoTable("users").Row(new { username = "2013/187803", email = "justrandom@gmail.com", password_hash = "$2a$13$8maVEm7hDUicYsMrz/pbsu9ixKrmQcUJm8xM4W/5LzXFAKtoab9Z6" });

            // inserts three types of roles to the roles table
            Insert.IntoTable("roles").Row(new { name = "admin" });
            Insert.IntoTable("roles").Row(new { name = "course adviser" });
            Insert.IntoTable("roles").Row(new { name = "student" });

            // inserts roles' ids and users' ids as F. Keys to role_users table
            Insert.IntoTable("role_users").Row(new { user_id = 1, role_id = 1 });
            Insert.IntoTable("role_users").Row(new { user_id = 2, role_id = 2 });
            Insert.IntoTable("role_users").Row(new { user_id = 3, role_id = 3 });
            Insert.IntoTable("role_users").Row(new { user_id = 4, role_id = 3 });
        }
    }
}
