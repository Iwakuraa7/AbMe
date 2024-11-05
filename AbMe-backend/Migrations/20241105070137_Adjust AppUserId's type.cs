using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbMe_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdjustAppUserIdstype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserColors_AspNetUsers_AppUserId1",
                table: "UserColors");

            migrationBuilder.DropIndex(
                name: "IX_UserColors_AppUserId1",
                table: "UserColors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2abe6a03-a4be-43f5-b897-81f8f20e8a43");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3f55788-41d3-4ee3-a453-038ff700cf6e");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "UserColors");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserColors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b1159e8-7927-45eb-b76a-8bd0549f8336", null, "Admin", "ADMIN" },
                    { "f6fdfe6f-99c0-4fd3-9e1a-f0356ddc5ed0", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserColors_AppUserId",
                table: "UserColors",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserColors_AspNetUsers_AppUserId",
                table: "UserColors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserColors_AspNetUsers_AppUserId",
                table: "UserColors");

            migrationBuilder.DropIndex(
                name: "IX_UserColors_AppUserId",
                table: "UserColors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b1159e8-7927-45eb-b76a-8bd0549f8336");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6fdfe6f-99c0-4fd3-9e1a-f0356ddc5ed0");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "UserColors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "UserColors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2abe6a03-a4be-43f5-b897-81f8f20e8a43", null, "Admin", "ADMIN" },
                    { "a3f55788-41d3-4ee3-a453-038ff700cf6e", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserColors_AppUserId1",
                table: "UserColors",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserColors_AspNetUsers_AppUserId1",
                table: "UserColors",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
