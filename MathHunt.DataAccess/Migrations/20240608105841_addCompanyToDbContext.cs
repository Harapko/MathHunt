using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHunt.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEntity_AspNetUsers_AppUserId",
                table: "CompanyEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEntity",
                table: "CompanyEntity");

            migrationBuilder.RenameTable(
                name: "CompanyEntity",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEntity_AppUserId",
                table: "Company",
                newName: "IX_Company_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "TradeName",
                table: "Company",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_AppUserId",
                table: "Company",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_AppUserId",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "CompanyEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Company_AppUserId",
                table: "CompanyEntity",
                newName: "IX_CompanyEntity_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "TradeName",
                table: "CompanyEntity",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEntity",
                table: "CompanyEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEntity_AspNetUsers_AppUserId",
                table: "CompanyEntity",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
