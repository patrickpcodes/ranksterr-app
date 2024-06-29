using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ranksterr.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class InitialwList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TmdbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImdbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieItem_Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieItem_ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovieItem_TmdbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieItem_ImdbId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListItemAssignments",
                columns: table => new
                {
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListItemId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItemAssignments", x => new { x.ListId, x.ListItemId });
                    table.ForeignKey(
                        name: "FK_ListItemAssignments_ItemLists_ListId",
                        column: x => x.ListId,
                        principalTable: "ItemLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListItemAssignments_ListItem_ListItemId",
                        column: x => x.ListItemId,
                        principalTable: "ListItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListItemAssignments_ListItem_ListItemId1",
                        column: x => x.ListItemId1,
                        principalTable: "ListItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListItemAssignments_ListItemId",
                table: "ListItemAssignments",
                column: "ListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItemAssignments_ListItemId1",
                table: "ListItemAssignments",
                column: "ListItemId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListItemAssignments");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "ItemLists");

            migrationBuilder.DropTable(
                name: "ListItem");
        }
    }
}
