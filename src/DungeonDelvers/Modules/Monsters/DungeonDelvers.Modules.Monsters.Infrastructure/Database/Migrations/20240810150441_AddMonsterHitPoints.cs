using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMonsterHitPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hit_points_expression",
                schema: "monsters",
                table: "monsters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hit_points_expression",
                schema: "monsters",
                table: "monsters");
        }
    }
}
