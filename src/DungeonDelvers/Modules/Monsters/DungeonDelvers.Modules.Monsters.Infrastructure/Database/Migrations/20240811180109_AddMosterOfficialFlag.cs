using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMosterOfficialFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "official",
                schema: "monsters",
                table: "monsters",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "official",
                schema: "monsters",
                table: "monsters");
        }
    }
}
