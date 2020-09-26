using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpeccableService.Backend.Database.Migrations
{
    public partial class EvolveMenuAndAddVenue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "section");

            migrationBuilder.CreateTable(
                name: "menu_section",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MenuId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menu_section_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "venue",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "menu_item",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MenuSectionId = table.Column<string>(nullable: true),
                    SectionId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ThumbnailImageSerialized = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menu_item_menu_section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "menu_section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_SectionId",
                table: "menu_item",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_menu_section_MenuId",
                table: "menu_section",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_item");

            migrationBuilder.DropTable(
                name: "venue");

            migrationBuilder.DropTable(
                name: "menu_section");

            migrationBuilder.CreateTable(
                name: "section",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    MenuId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
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
    }
}
