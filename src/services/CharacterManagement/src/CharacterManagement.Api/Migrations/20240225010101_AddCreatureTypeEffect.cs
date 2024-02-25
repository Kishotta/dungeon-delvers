using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatureTypeEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreatureTypeEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureTypeEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreatureTypeEffects_Effect_Id",
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
                name: "CreatureTypeEffects");
        }
    }
}
