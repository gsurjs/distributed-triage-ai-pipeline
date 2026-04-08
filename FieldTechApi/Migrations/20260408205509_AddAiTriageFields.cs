using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldTechApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAiTriageFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AiRecommendedAction",
                table: "Reports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RiskLevel",
                table: "Reports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AiRecommendedAction",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "Reports");
        }
    }
}
