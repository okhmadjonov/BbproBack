using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bbpro.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProjectChnaged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadLink",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadLink",
                table: "Projects");
        }
    }
}
