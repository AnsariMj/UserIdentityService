using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class check1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "30dfb410-a53b-4506-a6e3-7c68b45b1f7c", "1", "Admin", "Admin" },
                    { "74dc9e0d-18a2-456d-a809-b214803d2040", "3", "HR", "HR" },
                    { "eb02767a-bd69-42e3-92e3-1d48ffd85554", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
