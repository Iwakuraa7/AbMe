using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbMe_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAppUserIdtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicEntities_AspNetUsers_AppUserId1",
                table: "MusicEntities");

            migrationBuilder.DropIndex(
                name: "IX_MusicEntities_AppUserId1",
                table: "MusicEntities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a6beaf5-97e4-457b-a155-8fe8f8550883");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ef6a564-463f-4b1e-aede-107e40327e21");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "MusicEntities");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "MusicEntities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72007113-61f1-4993-8e59-59f451069968", null, "Admin", "ADMIN" },
                    { "da9681d5-8eeb-4f3a-8619-ac713ff20bcc", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicEntities_AppUserId",
                table: "MusicEntities",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicEntities_AspNetUsers_AppUserId",
                table: "MusicEntities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicEntities_AspNetUsers_AppUserId",
                table: "MusicEntities");

            migrationBuilder.DropIndex(
                name: "IX_MusicEntities_AppUserId",
                table: "MusicEntities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72007113-61f1-4993-8e59-59f451069968");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da9681d5-8eeb-4f3a-8619-ac713ff20bcc");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "MusicEntities",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "MusicEntities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a6beaf5-97e4-457b-a155-8fe8f8550883", null, "Admin", "ADMIN" },
                    { "8ef6a564-463f-4b1e-aede-107e40327e21", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicEntities_AppUserId1",
                table: "MusicEntities",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicEntities_AspNetUsers_AppUserId1",
                table: "MusicEntities",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
