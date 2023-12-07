using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RunningJac.IDP.Migrations
{
    public partial class AddedEmailFieldToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityCodeExpirationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6",
                column: "Email",
                value: "sommy@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf",
                column: "Email",
                value: "odi@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCodeExpirationDate",
                table: "Users");
        }
    }
}
