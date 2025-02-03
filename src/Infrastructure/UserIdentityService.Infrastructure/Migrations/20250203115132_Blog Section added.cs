using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BlogSectionadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08a6253a-d855-43ca-81b2-e3978a23fe98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23364e37-0856-446c-8d37-452a9dd01546");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "349bd790-cac3-4f1d-b1d8-bb7b9e191407");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Blogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08a6253a-d855-43ca-81b2-e3978a23fe98", "1", "Admin", "Admin" },
                    { "23364e37-0856-446c-8d37-452a9dd01546", "3", "HR", "HR" },
                    { "349bd790-cac3-4f1d-b1d8-bb7b9e191407", "2", "User", "User" }
                });
        }
    }
}
