using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserIdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addednulltorefreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshToeknExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToekn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshToeknExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToekn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
