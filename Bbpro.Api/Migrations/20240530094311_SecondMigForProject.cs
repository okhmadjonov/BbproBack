using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bbpro.Api.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigForProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    MapFrame = table.Column<string>(type: "text", nullable: false),
                    Address_UZ = table.Column<string>(type: "text", nullable: false),
                    Address_RU = table.Column<string>(type: "text", nullable: false),
                    Address_EN = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    WorkDay_UZ = table.Column<string>(type: "text", nullable: false),
                    WorkDay_RU = table.Column<string>(type: "text", nullable: false),
                    WorkDay_EN = table.Column<string>(type: "text", nullable: false),
                    Weekend_UZ = table.Column<string>(type: "text", nullable: false),
                    Weekend_RU = table.Column<string>(type: "text", nullable: false),
                    Weekend_EN = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Latests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Title_UZ = table.Column<string>(type: "text", nullable: false),
                    Title_RU = table.Column<string>(type: "text", nullable: false),
                    Title_EN = table.Column<string>(type: "text", nullable: false),
                    Description_UZ = table.Column<string>(type: "text", nullable: false),
                    Description_RU = table.Column<string>(type: "text", nullable: false),
                    Description_EN = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Latests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Title_UZ = table.Column<string>(type: "text", nullable: false),
                    Title_RU = table.Column<string>(type: "text", nullable: false),
                    Title_EN = table.Column<string>(type: "text", nullable: false),
                    Description_UZ = table.Column<string>(type: "text", nullable: false),
                    Description_RU = table.Column<string>(type: "text", nullable: false),
                    Description_EN = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Title_UZ = table.Column<string>(type: "text", nullable: false),
                    Title_RU = table.Column<string>(type: "text", nullable: false),
                    Title_EN = table.Column<string>(type: "text", nullable: false),
                    Description_UZ = table.Column<string>(type: "text", nullable: false),
                    Description_RU = table.Column<string>(type: "text", nullable: false),
                    Description_EN = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Latests");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Solutions");
        }
    }
}
