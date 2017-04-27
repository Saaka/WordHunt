using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WordHunt.Data.Migrations
{
    public partial class RenamedGameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ColumnsCount",
                table: "Games",
                newName: "BoardWidth");

            migrationBuilder.RenameColumn(
                name: "RowsCount",
                table: "Games",
                newName: "BoardHeight");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BoardHeight",
                table: "Games",
                newName: "RowsCount");

            migrationBuilder.RenameColumn(
                name: "BoardWidth",
                table: "Games",
                newName: "ColumnsCount");
        }
    }
}
