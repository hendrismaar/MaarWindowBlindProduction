﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaarWindowBlindProduction.Data.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patterns",
                table: "Patterns");

            migrationBuilder.RenameTable(
                name: "Patterns",
                newName: "Patterns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pattern",
                table: "Patterns",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pattern",
                table: "Patterns");

            migrationBuilder.RenameTable(
                name: "Patterns",
                newName: "Patterns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patterns",
                table: "Patterns",
                column: "Id");
        }
    }
}
