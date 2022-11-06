using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slacker.Infrastructure.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "265f46db-e462-4faf-b72a-88729fc20617");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e70ac3be-e355-4df5-ad15-0134f009648b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ede1adf6-f43e-48ae-8d88-0226bcb1ce22");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23b858c7-2fdd-4100-ba81-3d1b1d01bf0a", "e20c4490-95ff-4c4b-9c93-56ac4f6f3d9c", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2504ea37-5161-4b81-b6cc-5ba587d770f8", "178e8849-463b-49bb-9a00-1b8dded34186", "Default", "Default" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91e5cf7f-a647-4aa6-a033-eae3eea4d425", "75920c4a-d6b9-46a2-9713-18aa27a7b16c", "Manager", "Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23b858c7-2fdd-4100-ba81-3d1b1d01bf0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2504ea37-5161-4b81-b6cc-5ba587d770f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91e5cf7f-a647-4aa6-a033-eae3eea4d425");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "265f46db-e462-4faf-b72a-88729fc20617", "bf2390dc-a5ca-45d4-a27c-54a33f7706c7", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e70ac3be-e355-4df5-ad15-0134f009648b", "299f169f-31b6-4637-809d-68202b68316e", "Default", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ede1adf6-f43e-48ae-8d88-0226bcb1ce22", "e39d1e6c-be6c-47b5-b5dd-67a4eb96e5a5", "Manager", null });
        }
    }
}
