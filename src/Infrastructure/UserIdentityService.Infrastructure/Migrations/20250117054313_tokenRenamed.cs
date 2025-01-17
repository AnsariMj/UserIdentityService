using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tokenRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "548ff673-9442-4d2a-adc8-762b974b8914");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6c73c46-51fa-47f0-8317-3c0a0a0aa802");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d72f31ee-2b48-4541-9540-726e3de0d92a");

            migrationBuilder.RenameColumn(
                name: "RefreshToekn",
                table: "AspNetUsers",
                newName: "RefreshToken");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "AspNetUsers",
                newName: "RefreshToekn");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "548ff673-9442-4d2a-adc8-762b974b8914", "2", "User", "User" },
                    { "a6c73c46-51fa-47f0-8317-3c0a0a0aa802", "3", "HR", "HR" },
                    { "d72f31ee-2b48-4541-9540-726e3de0d92a", "1", "Admin", "Admin" }
                });
        }
    }
}
