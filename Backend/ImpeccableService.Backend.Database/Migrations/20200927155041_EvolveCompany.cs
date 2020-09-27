using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpeccableService.Backend.Database.Migrations
{
    public partial class EvolveCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_session_user_UserId",
                table: "session");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "user",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "session",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "company",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_OwnerId",
                table: "company",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_company_user_OwnerId",
                table: "company",
                column: "OwnerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_session_user_UserId",
                table: "session",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_user_OwnerId",
                table: "company");

            migrationBuilder.DropForeignKey(
                name: "FK_session_user_UserId",
                table: "session");

            migrationBuilder.DropIndex(
                name: "IX_company_OwnerId",
                table: "company");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "company");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "user",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "session",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_session_user_UserId",
                table: "session",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
