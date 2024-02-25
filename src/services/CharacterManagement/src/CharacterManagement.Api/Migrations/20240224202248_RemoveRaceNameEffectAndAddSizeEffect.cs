using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRaceNameEffectAndAddSizeEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceNameEffects");

            migrationBuilder.AddColumn<string>(
                name: "RaceName",
                table: "Characters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SizeEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SizeEffects_Effect_Id",
                        column: x => x.Id,
                        principalTable: "Effect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SizeEffects");

            migrationBuilder.DropColumn(
                name: "RaceName",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "RaceNameEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceNameEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceNameEffects_Effect_Id",
                        column: x => x.Id,
                        principalTable: "Effect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
