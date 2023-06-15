using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaarWindowBlindProduction.Data.Migrations
{
    public partial class ChangeGuidToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "WindowBlind");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "WindowBlind",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "WindowBlind");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "WindowBlind",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
