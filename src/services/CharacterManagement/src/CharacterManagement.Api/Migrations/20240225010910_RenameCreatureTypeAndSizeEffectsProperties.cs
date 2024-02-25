using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameCreatureTypeAndSizeEffectsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SizeEffects",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "CreatureTypeEffects",
                newName: "CreatureType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "SizeEffects",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "CreatureType",
                table: "CreatureTypeEffects",
                newName: "Value");
        }
    }
}
