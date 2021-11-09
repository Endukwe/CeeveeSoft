using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CeeveeSoftWebProj.Migrations
{
    public partial class migratn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skills_PortfolioId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_PortfolioId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Educations_PortfolioId",
                table: "Educations");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Educations",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PortfolioId",
                table: "Skills",
                column: "PortfolioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_PortfolioId",
                table: "Experiences",
                column: "PortfolioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_PortfolioId",
                table: "Educations",
                column: "PortfolioId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skills_PortfolioId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_PortfolioId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Educations_PortfolioId",
                table: "Educations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Educations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PortfolioId",
                table: "Skills",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_PortfolioId",
                table: "Experiences",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_PortfolioId",
                table: "Educations",
                column: "PortfolioId");
        }
    }
}
