using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slacker.Infrastructure.Migrations
{
    public partial class RemoveSignalRIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SigalRId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SigalRId",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
