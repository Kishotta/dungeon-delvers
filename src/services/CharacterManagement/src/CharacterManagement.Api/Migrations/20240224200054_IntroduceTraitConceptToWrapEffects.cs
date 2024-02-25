using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class IntroduceTraitConceptToWrapEffects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effect_Races_RaceId",
                table: "Effect");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceNameEffect_Effect_Id",
                table: "RaceNameEffect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceNameEffect",
                table: "RaceNameEffect");

            migrationBuilder.RenameTable(
                name: "RaceNameEffect",
                newName: "RaceNameEffects");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "Effect",
                newName: "TraitId");

            migrationBuilder.RenameIndex(
                name: "IX_Effect_RaceId",
                table: "Effect",
                newName: "IX_Effect_TraitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceNameEffects",
                table: "RaceNameEffects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Trait",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RaceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trait", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trait_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trait_RaceId",
                table: "Trait",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Effect_Trait_TraitId",
                table: "Effect",
                column: "TraitId",
                principalTable: "Trait",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceNameEffects_Effect_Id",
                table: "RaceNameEffects",
                column: "Id",
                principalTable: "Effect",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effect_Trait_TraitId",
                table: "Effect");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceNameEffects_Effect_Id",
                table: "RaceNameEffects");

            migrationBuilder.DropTable(
                name: "Trait");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceNameEffects",
                table: "RaceNameEffects");

            migrationBuilder.RenameTable(
                name: "RaceNameEffects",
                newName: "RaceNameEffect");

            migrationBuilder.RenameColumn(
                name: "TraitId",
                table: "Effect",
                newName: "RaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Effect_TraitId",
                table: "Effect",
                newName: "IX_Effect_RaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceNameEffect",
                table: "RaceNameEffect",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Effect_Races_RaceId",
                table: "Effect",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceNameEffect_Effect_Id",
                table: "RaceNameEffect",
                column: "Id",
                principalTable: "Effect",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
