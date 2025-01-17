using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30dfb410-a53b-4506-a6e3-7c68b45b1f7c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74dc9e0d-18a2-456d-a809-b214803d2040");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb02767a-bd69-42e3-92e3-1d48ffd85554");

            migrationBuilder.RenameColumn(
                name: "RefreshToeknExpiry",
                table: "AspNetUsers",
                newName: "RefreshTokenExpiry");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20402d8c-6b3c-42c6-8457-0c1110846616", "3", "HR", "HR" },
                    { "9e17c5b6-2e43-4223-a347-c1d772d5dbc2", "2", "User", "User" },
                    { "bf2591cc-07d3-4ead-8693-98e329bbe0a9", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20402d8c-6b3c-42c6-8457-0c1110846616");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e17c5b6-2e43-4223-a347-c1d772d5dbc2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf2591cc-07d3-4ead-8693-98e329bbe0a9");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                newName: "RefreshToeknExpiry");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30dfb410-a53b-4506-a6e3-7c68b45b1f7c", "1", "Admin", "Admin" },
                    { "74dc9e0d-18a2-456d-a809-b214803d2040", "3", "HR", "HR" },
                    { "eb02767a-bd69-42e3-92e3-1d48ffd85554", "2", "User", "User" }
                });
        }
    }
}
