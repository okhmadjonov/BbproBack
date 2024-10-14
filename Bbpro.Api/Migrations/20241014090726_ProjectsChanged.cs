using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bbpro.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProjectsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadLink_EN",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DownloadLink_RU",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "DownloadLink_UZ",
                table: "Projects",
                newName: "DownloadLink");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DownloadLink",
                table: "Projects",
                newName: "DownloadLink_UZ");

            migrationBuilder.AddColumn<string>(
                name: "DownloadLink_EN",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DownloadLink_RU",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
