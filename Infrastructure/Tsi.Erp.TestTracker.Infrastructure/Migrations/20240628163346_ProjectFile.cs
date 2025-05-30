using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsi.Erp.TestTracker.Infrastructure.Migrations
{
    public partial class ProjectFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Assembly",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Assembly",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "AssemblyBytes",
                table: "Assembly",
                newName: "Data");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Assembly",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Assembly",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Assembly",
                newName: "AssemblyBytes");
        }
    }
}
