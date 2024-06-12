using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHunt.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPhotoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoUserEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    AppUserEntityId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoUserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoUserEntity_AspNetUsers_AppUserEntityId",
                        column: x => x.AppUserEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUserEntity_AppUserEntityId",
                table: "PhotoUserEntity",
                column: "AppUserEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoUserEntity");
        }
    }
}
