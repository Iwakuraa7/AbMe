using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbMe_backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserColormodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5094903-0467-46ed-810d-b70669cb4ff6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5f19769-8de4-446b-a459-337ef4c2f3f2");

            migrationBuilder.CreateTable(
                name: "UserColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserColors_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56b35078-403d-4962-8ea5-87b07838074c", null, "Admin", "ADMIN" },
                    { "abc4a65a-d6b1-4707-b78a-d609556e9ca0", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserColors_AppUserId1",
                table: "UserColors",
                column: "AppUserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserColors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56b35078-403d-4962-8ea5-87b07838074c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc4a65a-d6b1-4707-b78a-d609556e9ca0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c5094903-0467-46ed-810d-b70669cb4ff6", null, "User", "USER" },
                    { "f5f19769-8de4-446b-a459-337ef4c2f3f2", null, "Admin", "ADMIN" }
                });
        }
    }
}
