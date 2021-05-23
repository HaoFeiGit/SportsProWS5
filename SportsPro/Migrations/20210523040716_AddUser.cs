using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsPro.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "Password", "Role", "Username" },
                values: new object[] { 1, "John Doe", "P@ssw0rd", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "Password", "Role", "Username" },
                values: new object[] { 2, "John Doe", "P@ssw0rd", "Technician", "tech" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
