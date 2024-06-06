using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHunt.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDescriptionSkillToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionSkill",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionSkill",
                table: "AspNetUsers");
        }
    }
}
