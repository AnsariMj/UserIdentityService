using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
