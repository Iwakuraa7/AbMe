using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbMe_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdddefaultvaluestoUserColormodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { "2abe6a03-a4be-43f5-b897-81f8f20e8a43", null, "Admin", "ADMIN" },
                    { "a3f55788-41d3-4ee3-a453-038ff700cf6e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2abe6a03-a4be-43f5-b897-81f8f20e8a43");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3f55788-41d3-4ee3-a453-038ff700cf6e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56b35078-403d-4962-8ea5-87b07838074c", null, "Admin", "ADMIN" },
                    { "abc4a65a-d6b1-4707-b78a-d609556e9ca0", null, "User", "USER" }
                });
        }
    }
}
