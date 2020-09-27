using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpeccableService.Backend.Database.Migrations
{
    public partial class BindVenueToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "company_id",
                table: "venue",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_venue_company_id",
                table: "venue",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "fk_venue_company_company_id",
                table: "venue",
                column: "company_id",
                principalTable: "company",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_venue_company_company_id",
                table: "venue");

            migrationBuilder.DropIndex(
                name: "ix_venue_company_id",
                table: "venue");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "venue");
        }
    }
}
