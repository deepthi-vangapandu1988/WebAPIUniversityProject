using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class addingusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Fullname", "Password", "Role", "Username" },
                values: new object[] { 1, "Student 1", "Password1", "Student", "Student1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Fullname", "Password", "Role", "Username" },
                values: new object[] { 2, "Student 2", "Password2", "Student", "Student2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Fullname", "Password", "Role", "Username" },
                values: new object[] { 3, "Student 3", "Password3", "Student", "Student3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
