using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsi.Erp.TestTracker.Infrastructure.Migrations
{
    public partial class testerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitorings_Users_TesterId",
                table: "Monitorings");

            migrationBuilder.AlterColumn<string>(
                name: "TesterId",
                table: "Monitorings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitorings_TsiUsers_TesterId",
                table: "Monitorings",
                column: "TesterId",
                principalTable: "TsiUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitorings_TsiUsers_TesterId",
                table: "Monitorings");

            migrationBuilder.AlterColumn<int>(
                name: "TesterId",
                table: "Monitorings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Monitorings_Users_TesterId",
                table: "Monitorings",
                column: "TesterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
