using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherApiTeddySmith.Migrations
{
    /// <inheritdoc />
    public partial class addCommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24088dba-721d-45e3-9286-4d817879e12e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "871f4df4-30bf-44c8-9ff6-478202f84f74");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9aac507a-a9d3-4b84-a2da-326149504842", null, "User", "USER" },
                    { "d242f829-1ebd-49f3-a4d0-a59a09d9e3d9", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aac507a-a9d3-4b84-a2da-326149504842");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d242f829-1ebd-49f3-a4d0-a59a09d9e3d9");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Comment");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24088dba-721d-45e3-9286-4d817879e12e", null, "User", "USER" },
                    { "871f4df4-30bf-44c8-9ff6-478202f84f74", null, "Admin", "ADMIN" }
                });
        }
    }
}
