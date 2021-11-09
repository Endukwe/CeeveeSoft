using Microsoft.EntityFrameworkCore.Migrations;

namespace CeeveeSoftWebProj.Migrations
{
    public partial class IdentityIdprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Portfolios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Portfolios");
        }
    }
}
