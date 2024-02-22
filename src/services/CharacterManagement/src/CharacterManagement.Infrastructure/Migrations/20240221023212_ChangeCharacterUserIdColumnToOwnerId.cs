using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCharacterUserIdColumnToOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Characters",
                newName: "OwnerId");

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Characters",
                newName: "UserId");
        }
    }
}
