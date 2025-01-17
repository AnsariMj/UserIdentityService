using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01f38761-1b40-4664-8268-7e7bd2071b1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbbf04c6-b8d5-4a99-b20d-59048bde8895");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbc85f45-1be2-4396-9a3d-f0fa00b30d8a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c87c87d-0d58-45d7-931b-f4d159d427b5", "3", "HR", "HR" },
                    { "853c9e92-b9f1-46ce-8db4-25b1fedfe473", "1", "Admin", "Admin" },
                    { "fb808240-26a2-481b-a124-6d149b3dfd4a", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c87c87d-0d58-45d7-931b-f4d159d427b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "853c9e92-b9f1-46ce-8db4-25b1fedfe473");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb808240-26a2-481b-a124-6d149b3dfd4a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01f38761-1b40-4664-8268-7e7bd2071b1f", "1", "Admin", "Admin" },
                    { "bbbf04c6-b8d5-4a99-b20d-59048bde8895", "2", "User", "User" },
                    { "bbc85f45-1be2-4396-9a3d-f0fa00b30d8a", "3", "HR", "HR" }
                });
        }
    }
}
