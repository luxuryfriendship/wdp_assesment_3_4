using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreWebSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class rename1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopicTitle",
                table: "AIImage",
                newName: "Prompt");

            migrationBuilder.RenameColumn(
                name: "PostDate",
                table: "AIImage",
                newName: "UploadDate");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "AIImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AIImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "AIImage");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AIImage");

            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "AIImage",
                newName: "PostDate");

            migrationBuilder.RenameColumn(
                name: "Prompt",
                table: "AIImage",
                newName: "TopicTitle");
        }
    }
}
