using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHunt.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPhotoUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoUserEntity_AspNetUsers_AppUserEntityId",
                table: "PhotoUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoUserEntity",
                table: "PhotoUserEntity");

            migrationBuilder.RenameTable(
                name: "PhotoUserEntity",
                newName: "PhotoUser");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoUserEntity_AppUserEntityId",
                table: "PhotoUser",
                newName: "IX_PhotoUser_AppUserEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoUser",
                table: "PhotoUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoUser_AspNetUsers_AppUserEntityId",
                table: "PhotoUser",
                column: "AppUserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoUser_AspNetUsers_AppUserEntityId",
                table: "PhotoUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoUser",
                table: "PhotoUser");

            migrationBuilder.RenameTable(
                name: "PhotoUser",
                newName: "PhotoUserEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoUser_AppUserEntityId",
                table: "PhotoUserEntity",
                newName: "IX_PhotoUserEntity_AppUserEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoUserEntity",
                table: "PhotoUserEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoUserEntity_AspNetUsers_AppUserEntityId",
                table: "PhotoUserEntity",
                column: "AppUserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
