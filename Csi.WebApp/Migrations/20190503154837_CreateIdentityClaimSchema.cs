using Microsoft.EntityFrameworkCore.Migrations;

namespace Csi.WebApp.Migrations
{
    public partial class CreateIdentityClaimSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "TwoFactorEnabled",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "PhoneNumberConfirmed",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "LockoutEnabled",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "EmailConfirmed",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.CreateTable(
                name: "IdentityUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaims", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUserClaims");

            migrationBuilder.AlterColumn<short>(
                name: "TwoFactorEnabled",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "PhoneNumberConfirmed",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "LockoutEnabled",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "EmailConfirmed",
                table: "CsiUsers",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
