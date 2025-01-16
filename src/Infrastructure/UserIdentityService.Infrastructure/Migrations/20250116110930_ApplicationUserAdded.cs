using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78155b27-b976-401b-8d12-b8bcf45f8fb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be7f0097-3e41-4d8d-b4a5-5da44df6efe9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2f0b507-b6ff-4b91-8c9d-cd8bc361d6e5");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToekn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshToeknExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7de5bf23-19b4-4344-9ec0-1fed1bb966a7", "2", "User", "User" },
                    { "8c051b51-660f-4b9b-b955-0b4a0e81511b", "1", "Admin", "Admin" },
                    { "fd4311c9-c50f-42fc-8c9c-34f0c9f2724c", "3", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7de5bf23-19b4-4344-9ec0-1fed1bb966a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c051b51-660f-4b9b-b955-0b4a0e81511b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd4311c9-c50f-42fc-8c9c-34f0c9f2724c");

            migrationBuilder.DropColumn(
                name: "RefreshToekn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToeknExpiry",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78155b27-b976-401b-8d12-b8bcf45f8fb0", "1", "Admin", "Admin" },
                    { "be7f0097-3e41-4d8d-b4a5-5da44df6efe9", "3", "HR", "HR" },
                    { "e2f0b507-b6ff-4b91-8c9d-cd8bc361d6e5", "2", "User", "User" }
                });
        }
    }
}
