using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPAssessment3.Migrations
{
    /// <inheritdoc />
    public partial class renamemessagecontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageContent",
                table: "AIImage",
                newName: "ImageGenerator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageGenerator",
                table: "AIImage",
                newName: "MessageContent");
        }
    }
}
