using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUN_Digital_Speaker.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAmendments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClauseNumber",
                table: "Amendment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClauseNumber",
                table: "Amendment");
        }
    }
}
