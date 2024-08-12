using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMonsterSizeTypeAlignmentAndArmorClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "alignment",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "armor_class",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "size",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alignment",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "armor_class",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "size",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "monsters",
                table: "monsters");
        }
    }
}
