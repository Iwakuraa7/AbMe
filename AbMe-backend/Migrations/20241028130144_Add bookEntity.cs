using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbMe_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddbookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookEntity_AspNetUsers_AppUserId",
                table: "BookEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookEntity",
                table: "BookEntity");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c873fbd-307c-41b1-9ac6-988fd2f7ec62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce74f6a3-291c-4690-8206-b9b4e78f1f3a");

            migrationBuilder.RenameTable(
                name: "BookEntity",
                newName: "BookEntities");

            migrationBuilder.RenameIndex(
                name: "IX_BookEntity_AppUserId",
                table: "BookEntities",
                newName: "IX_BookEntities_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookEntities",
                table: "BookEntities",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22129590-d5cb-4fc3-b963-96efcba45656", null, "Admin", "ADMIN" },
                    { "a4f8a2d6-ee7e-43d7-b3c8-db4f5cfde17a", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookEntities_AspNetUsers_AppUserId",
                table: "BookEntities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookEntities_AspNetUsers_AppUserId",
                table: "BookEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookEntities",
                table: "BookEntities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22129590-d5cb-4fc3-b963-96efcba45656");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4f8a2d6-ee7e-43d7-b3c8-db4f5cfde17a");

            migrationBuilder.RenameTable(
                name: "BookEntities",
                newName: "BookEntity");

            migrationBuilder.RenameIndex(
                name: "IX_BookEntities_AppUserId",
                table: "BookEntity",
                newName: "IX_BookEntity_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookEntity",
                table: "BookEntity",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c873fbd-307c-41b1-9ac6-988fd2f7ec62", null, "Admin", "ADMIN" },
                    { "ce74f6a3-291c-4690-8206-b9b4e78f1f3a", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookEntity_AspNetUsers_AppUserId",
                table: "BookEntity",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
