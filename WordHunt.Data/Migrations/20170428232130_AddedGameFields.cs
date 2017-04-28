using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WordHunt.Data.Migrations
{
    public partial class AddedGameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Checked = table.Column<bool>(nullable: false),
                    CheckedByTeamId = table.Column<int>(nullable: true),
                    ColumnIndex = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    RowIndex = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFields_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameFields_GameId",
                table: "GameFields",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameFields");
        }
    }
}
