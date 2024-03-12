using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boxfusion.LMS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreatePreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    SecondaryCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    TertiaryCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preferences_Categories_PrimaryCategoryId",
                        column: x => x.PrimaryCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Preferences_Categories_SecondaryCategoryId",
                        column: x => x.SecondaryCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Preferences_Categories_TertiaryCategoryId",
                        column: x => x.TertiaryCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Preferences_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_PatronId",
                table: "Preferences",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_PrimaryCategoryId",
                table: "Preferences",
                column: "PrimaryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_SecondaryCategoryId",
                table: "Preferences",
                column: "SecondaryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_TertiaryCategoryId",
                table: "Preferences",
                column: "TertiaryCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preferences");
        }
    }
}
