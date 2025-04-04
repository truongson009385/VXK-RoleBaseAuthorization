using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoleBaseAuthorization.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdTableComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppUserId1",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "559fbfc2-cb26-48e7-9afa-26d3464999ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e25e1bdd-278b-48e9-9f89-ca0236ad1ed4");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d00bc07-2a99-497d-8e79-68014d8694a1", null, "Admin", "ADMIN" },
                    { "a8a9fc73-92a5-4fb7-ad6a-943a93b4342c", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d00bc07-2a99-497d-8e79-68014d8694a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8a9fc73-92a5-4fb7-ad6a-943a93b4342c");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "559fbfc2-cb26-48e7-9afa-26d3464999ab", null, "Admin", "ADMIN" },
                    { "e25e1bdd-278b-48e9-9f89-ca0236ad1ed4", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserId1",
                table: "Comments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId1",
                table: "Comments",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
