using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUN_Digital_Speaker.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToDelegation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "Delegation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Delegation");
        }
    }
}
