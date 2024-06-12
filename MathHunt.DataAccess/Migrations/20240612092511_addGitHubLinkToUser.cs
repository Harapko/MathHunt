using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHunt.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addGitHubLinkToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GitHubLink",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHubLink",
                table: "AspNetUsers");
        }
    }
}
