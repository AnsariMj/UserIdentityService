using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07a820ac-5b66-4f84-9670-1d30157dfdf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8968c4b1-391b-40c8-a38f-6de73a2c96c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5a2f5da-9ba9-4e46-9c24-659b396aacf5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a0de5ef-4d85-4a17-8d3f-5a25172b3f91", "1", "Admin", "Admin" },
                    { "d1520a2b-3f52-4b82-ba70-f5feac76e0a4", "3", "HR", "HR" },
                    { "ec39be2c-01e0-427e-9377-81d6e6569245", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a0de5ef-4d85-4a17-8d3f-5a25172b3f91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1520a2b-3f52-4b82-ba70-f5feac76e0a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec39be2c-01e0-427e-9377-81d6e6569245");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07a820ac-5b66-4f84-9670-1d30157dfdf0", "3", "HR", "HR" },
                    { "8968c4b1-391b-40c8-a38f-6de73a2c96c5", "1", "Admin", "Admin" },
                    { "e5a2f5da-9ba9-4e46-9c24-659b396aacf5", "2", "User", "User" }
                });
        }
    }
}
