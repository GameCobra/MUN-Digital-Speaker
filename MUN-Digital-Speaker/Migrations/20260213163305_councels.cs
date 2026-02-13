using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUN_Digital_Speaker.Migrations
{
    /// <inheritdoc />
    public partial class councels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedToSpeak",
                table: "Delegation");

            migrationBuilder.AddColumn<bool>(
                name: "RequestedToSpeakECO",
                table: "Delegation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestedToSpeakENV",
                table: "Delegation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestedToSpeakGEN",
                table: "Delegation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestedToSpeakHE",
                table: "Delegation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestedToSpeakSEC",
                table: "Delegation",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedToSpeakECO",
                table: "Delegation");

            migrationBuilder.DropColumn(
                name: "RequestedToSpeakENV",
                table: "Delegation");

            migrationBuilder.DropColumn(
                name: "RequestedToSpeakGEN",
                table: "Delegation");

            migrationBuilder.DropColumn(
                name: "RequestedToSpeakHE",
                table: "Delegation");

            migrationBuilder.DropColumn(
                name: "RequestedToSpeakSEC",
                table: "Delegation");

            migrationBuilder.AddColumn<bool>(
                name: "RequestedToSpeak",
                table: "Delegation",
                type: "bit",
                nullable: true);
        }
    }
}
