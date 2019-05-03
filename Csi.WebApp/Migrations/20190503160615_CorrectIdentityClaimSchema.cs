using Microsoft.EntityFrameworkCore.Migrations;

namespace Csi.WebApp.Migrations
{
    public partial class CorrectIdentityClaimSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "TwoFactorEnabled",
                table: "CsiUsers",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "PhoneNumberConfirmed",
                table: "CsiUsers",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "LockoutEnabled",
                table: "CsiUsers",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "EmailConfirmed",
                table: "CsiUsers",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "TwoFactorEnabled",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "bit(1)");

            migrationBuilder.AlterColumn<short>(
                name: "PhoneNumberConfirmed",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "bit(1)");

            migrationBuilder.AlterColumn<short>(
                name: "LockoutEnabled",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "bit(1)");

            migrationBuilder.AlterColumn<short>(
                name: "EmailConfirmed",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "bit(1)");
        }
    }
}
