using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaarWindowBlindProduction.Data.Migrations
{
    public partial class CreateNewViewModelCreateOrderViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatternNumber",
                table: "WindowBlind",
                newName: "PatternId");

            migrationBuilder.CreateIndex(
                name: "IX_WindowBlind_PatternId",
                table: "WindowBlind",
                column: "PatternId");

            migrationBuilder.AddForeignKey(
                name: "FK_WindowBlind_Patterns_PatternId",
                table: "WindowBlind",
                column: "PatternId",
                principalTable: "Patterns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WindowBlind_Patterns_PatternId",
                table: "WindowBlind");

            migrationBuilder.DropIndex(
                name: "IX_WindowBlind_PatternId",
                table: "WindowBlind");

            migrationBuilder.RenameColumn(
                name: "PatternId",
                table: "WindowBlind",
                newName: "PatternNumber");
        }
    }
}
