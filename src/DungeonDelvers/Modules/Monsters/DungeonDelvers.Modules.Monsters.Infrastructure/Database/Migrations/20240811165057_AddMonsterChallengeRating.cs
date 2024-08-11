using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMonsterChallengeRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "challenge_rating",
                schema: "monsters",
                table: "monsters",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "challenge_rating",
                schema: "monsters",
                table: "monsters");
        }
    }
}
