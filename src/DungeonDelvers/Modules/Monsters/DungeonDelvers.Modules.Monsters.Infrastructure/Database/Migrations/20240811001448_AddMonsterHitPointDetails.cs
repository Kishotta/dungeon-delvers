using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMonsterHitPointDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hit_points_average",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hit_points_dice_count",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hit_points_dice_type",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hit_points_maximum",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hit_points_minimum",
                schema: "monsters",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hit_points_modifier",
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
                name: "hit_points_average",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "hit_points_dice_count",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "hit_points_dice_type",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "hit_points_maximum",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "hit_points_minimum",
                schema: "monsters",
                table: "monsters");

            migrationBuilder.DropColumn(
                name: "hit_points_modifier",
                schema: "monsters",
                table: "monsters");
        }
    }
}
