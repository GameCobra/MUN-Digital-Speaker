using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUN_Digital_Speaker.Migrations
{
    /// <inheritdoc />
    public partial class AddedAmendmentPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmendmentPoints",
                table: "Delegation",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmendmentPoints",
                table: "Delegation");
        }
    }
}
