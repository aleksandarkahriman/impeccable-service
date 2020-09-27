using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpeccableService.Backend.Database.Migrations
{
    public partial class MigrateColumnNamingToUnderscoreConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_user_OwnerId",
                table: "company");

            migrationBuilder.DropForeignKey(
                name: "FK_menu_item_menu_section_SectionId",
                table: "menu_item");

            migrationBuilder.DropForeignKey(
                name: "FK_menu_section_menu_MenuId",
                table: "menu_section");

            migrationBuilder.DropForeignKey(
                name: "FK_session_user_UserId",
                table: "session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_venue",
                table: "venue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_session",
                table: "session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_menu_section",
                table: "menu_section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_menu_item",
                table: "menu_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_menu",
                table: "menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company",
                table: "company");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "venue",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "venue",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "user",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProfileImageSerialized",
                table: "user",
                newName: "profile_image_serialized");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "user",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "session",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "session",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "session",
                newName: "refresh_token");

            migrationBuilder.RenameColumn(
                name: "LogoutToken",
                table: "session",
                newName: "logout_token");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "session",
                newName: "access_token");

            migrationBuilder.RenameIndex(
                name: "IX_session_UserId",
                table: "session",
                newName: "ix_session_user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "menu_section",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "menu_section",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "menu_section",
                newName: "menu_id");

            migrationBuilder.RenameIndex(
                name: "IX_menu_section_MenuId",
                table: "menu_section",
                newName: "ix_menu_section_menu_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "menu_item",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "menu_item",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ThumbnailImageSerialized",
                table: "menu_item",
                newName: "thumbnail_image_serialized");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "menu_item",
                newName: "section_id");

            migrationBuilder.RenameColumn(
                name: "MenuSectionId",
                table: "menu_item",
                newName: "menu_section_id");

            migrationBuilder.RenameIndex(
                name: "IX_menu_item_SectionId",
                table: "menu_item",
                newName: "ix_menu_item_section_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "menu",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "menu",
                newName: "venue_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "company",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "company",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "company",
                newName: "owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_company_OwnerId",
                table: "company",
                newName: "ix_company_owner_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_venue",
                table: "venue",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_session",
                table: "session",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_section",
                table: "menu_section",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu_item",
                table: "menu_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_menu",
                table: "menu",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_company",
                table: "company",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_company_user_owner_id",
                table: "company",
                column: "owner_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_menu_section_section_id",
                table: "menu_item",
                column: "section_id",
                principalTable: "menu_section",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_section_menu_menu_id",
                table: "menu_section",
                column: "menu_id",
                principalTable: "menu",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_session_user_user_id",
                table: "session",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_user_owner_id",
                table: "company");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_menu_section_section_id",
                table: "menu_item");

            migrationBuilder.DropForeignKey(
                name: "fk_menu_section_menu_menu_id",
                table: "menu_section");

            migrationBuilder.DropForeignKey(
                name: "fk_session_user_user_id",
                table: "session");

            migrationBuilder.DropPrimaryKey(
                name: "pk_venue",
                table: "venue");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_session",
                table: "session");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_section",
                table: "menu_section");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu_item",
                table: "menu_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_menu",
                table: "menu");

            migrationBuilder.DropPrimaryKey(
                name: "pk_company",
                table: "company");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "venue",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "venue",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "user",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "profile_image_serialized",
                table: "user",
                newName: "ProfileImageSerialized");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "user",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "session",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "session",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "refresh_token",
                table: "session",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "logout_token",
                table: "session",
                newName: "LogoutToken");

            migrationBuilder.RenameColumn(
                name: "access_token",
                table: "session",
                newName: "AccessToken");

            migrationBuilder.RenameIndex(
                name: "ix_session_user_id",
                table: "session",
                newName: "IX_session_UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "menu_section",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "menu_section",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "menu_id",
                table: "menu_section",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_section_menu_id",
                table: "menu_section",
                newName: "IX_menu_section_MenuId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "menu_item",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "menu_item",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "thumbnail_image_serialized",
                table: "menu_item",
                newName: "ThumbnailImageSerialized");

            migrationBuilder.RenameColumn(
                name: "section_id",
                table: "menu_item",
                newName: "SectionId");

            migrationBuilder.RenameColumn(
                name: "menu_section_id",
                table: "menu_item",
                newName: "MenuSectionId");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_section_id",
                table: "menu_item",
                newName: "IX_menu_item_SectionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "menu",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "venue_id",
                table: "menu",
                newName: "VenueId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "company",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "company",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "company",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "ix_company_owner_id",
                table: "company",
                newName: "IX_company_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_venue",
                table: "venue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_session",
                table: "session",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_menu_section",
                table: "menu_section",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_menu_item",
                table: "menu_item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_menu",
                table: "menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company",
                table: "company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_company_user_OwnerId",
                table: "company",
                column: "OwnerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_item_menu_section_SectionId",
                table: "menu_item",
                column: "SectionId",
                principalTable: "menu_section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_section_menu_MenuId",
                table: "menu_section",
                column: "MenuId",
                principalTable: "menu",
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
    }
}
