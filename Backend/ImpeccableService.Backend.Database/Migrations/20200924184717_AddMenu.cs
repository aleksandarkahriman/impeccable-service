using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpeccableService.Backend.Database.Migrations
{
    public partial class AddMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    VenueId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "section",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MenuId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_section_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_section_MenuId",
                table: "section",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "section");

            migrationBuilder.DropTable(
                name: "menu");
        }
    }
}
