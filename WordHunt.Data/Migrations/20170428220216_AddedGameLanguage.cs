using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WordHunt.Data.Migrations
{
    public partial class AddedGameLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_LanguageId",
                table: "Games",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Languages_LanguageId",
                table: "Games",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Languages_LanguageId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_LanguageId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Games");
        }
    }
}
