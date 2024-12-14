using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_gestion.Migrations
{
    /// <inheritdoc />
    public partial class GestionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deuda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deuda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deuda_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cobro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeudaId = table.Column<int>(type: "int", nullable: false),
                    MontoCobrado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCobro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cobro_Deuda_DeudaId",
                        column: x => x.DeudaId,
                        principalTable: "Deuda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobro_DeudaId",
                table: "Cobro",
                column: "DeudaId");

            migrationBuilder.CreateIndex(
                name: "IX_Deuda_ClienteId",
                table: "Deuda",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cobro");

            migrationBuilder.DropTable(
                name: "Deuda");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
