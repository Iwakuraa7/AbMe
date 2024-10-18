using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbMe_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f27c453-382c-48bc-8af3-785889a3b59f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f07ffa81-3b8c-4b61-b6f7-3a0fd58432da");

            migrationBuilder.CreateTable(
                name: "MusicEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicEntities_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicEntities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a6beaf5-97e4-457b-a155-8fe8f8550883");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ef6a564-463f-4b1e-aede-107e40327e21");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8f27c453-382c-48bc-8af3-785889a3b59f", null, "Admin", "ADMIN" },
                    { "f07ffa81-3b8c-4b61-b6f7-3a0fd58432da", null, "User", "USER" }
                });
        }
    }
}
