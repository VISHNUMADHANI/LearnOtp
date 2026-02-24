using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homecare_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddResetTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Admins",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Admins",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiry",
                table: "Admins",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiry",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Admins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);
        }
    }
}
