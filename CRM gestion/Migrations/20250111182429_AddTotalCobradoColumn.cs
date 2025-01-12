using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_gestion.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalCobradoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCobrado",
                table: "Deudas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCobrado",
                table: "Deudas");
        }
    }
}
