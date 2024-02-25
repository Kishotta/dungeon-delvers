using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class TryTptMappingStrategyForEffects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Effect");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Effect");

            migrationBuilder.CreateTable(
                name: "RaceNameEffect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceNameEffect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceNameEffect_Effect_Id",
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
                name: "RaceNameEffect");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Effect",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Effect",
                type: "text",
                nullable: true);
        }
    }
}
