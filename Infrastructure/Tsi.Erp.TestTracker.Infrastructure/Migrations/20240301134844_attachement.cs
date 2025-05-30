using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tsi.Erp.TestTracker.Infrastructure.Migrations
{
    public partial class attachement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachements_TsiUsers_UserId",
                table: "Attachements");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Attachements");

            migrationBuilder.RenameColumn(
                name: "RelatedObject",
                table: "Attachements",
                newName: "FileSize");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Attachements",
                newName: "Folder");

            migrationBuilder.RenameColumn(
                name: "KeyGroup",
                table: "Attachements",
                newName: "FileName");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Attachements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "Attachements",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachements_TsiUsers_UserId",
                table: "Attachements",
                column: "UserId",
                principalTable: "TsiUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachements_TsiUsers_UserId",
                table: "Attachements");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "Attachements");

            migrationBuilder.RenameColumn(
                name: "Folder",
                table: "Attachements",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FileSize",
                table: "Attachements",
                newName: "RelatedObject");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Attachements",
                newName: "KeyGroup");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Attachements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Attachements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachements_TsiUsers_UserId",
                table: "Attachements",
                column: "UserId",
                principalTable: "TsiUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
