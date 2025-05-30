using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsi.Erp.TestTracker.Infrastructure.Migrations
{
    public partial class ResponsibleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleId",
                table: "Monitorings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monitorings_ResponsibleId",
                table: "Monitorings",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitorings_TsiUsers_ResponsibleId",
                table: "Monitorings",
                column: "ResponsibleId",
                principalTable: "TsiUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitorings_TsiUsers_ResponsibleId",
                table: "Monitorings");

            migrationBuilder.DropIndex(
                name: "IX_Monitorings_ResponsibleId",
                table: "Monitorings");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Monitorings");
        }
    }
}
