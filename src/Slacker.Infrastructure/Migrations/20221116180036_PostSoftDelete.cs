using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slacker.Infrastructure.Migrations
{
    public partial class PostSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Posts");
        }
    }
}
