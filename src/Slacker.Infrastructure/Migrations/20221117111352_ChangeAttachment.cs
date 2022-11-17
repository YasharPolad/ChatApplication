using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slacker.Infrastructure.Migrations
{
    public partial class ChangeAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Attachment",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Attachment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
