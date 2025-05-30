using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsi.Erp.TestTracker.Infrastructure.Migrations
{
    public partial class ProjectFile_ProjectName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Assembly",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Assembly");
        }
    }
}
