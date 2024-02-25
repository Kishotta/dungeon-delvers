using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMovementSpeedEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovementSpeedEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementSpeedEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementSpeedEffects_Effect_Id",
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
                name: "MovementSpeedEffects");
        }
    }
}
