using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ranksterr.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class TvEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "ListItem",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<string>(
                name: "TvShowItem_ImdbId",
                table: "ListItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TvShowItem_ReleaseDate",
                table: "ListItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TvShowItem_Thumbnail",
                table: "ListItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TvShowItem_TmdbId",
                table: "ListItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TvShowItem_ImdbId",
                table: "ListItem");

            migrationBuilder.DropColumn(
                name: "TvShowItem_ReleaseDate",
                table: "ListItem");

            migrationBuilder.DropColumn(
                name: "TvShowItem_Thumbnail",
                table: "ListItem");

            migrationBuilder.DropColumn(
                name: "TvShowItem_TmdbId",
                table: "ListItem");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "ListItem",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);
        }
    }
}
