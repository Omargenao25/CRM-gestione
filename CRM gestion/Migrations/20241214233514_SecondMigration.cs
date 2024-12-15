using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_gestion.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobro_Deuda_DeudaId",
                table: "Cobro");

            migrationBuilder.DropForeignKey(
                name: "FK_Deuda_Cliente_ClienteId",
                table: "Deuda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deuda",
                table: "Deuda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cobro",
                table: "Cobro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Deuda",
                newName: "Deudas");

            migrationBuilder.RenameTable(
                name: "Cobro",
                newName: "Cobros");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_Deuda_ClienteId",
                table: "Deudas",
                newName: "IX_Deudas_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Cobro_DeudaId",
                table: "Cobros",
                newName: "IX_Cobros_DeudaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deudas",
                table: "Deudas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cobros",
                table: "Cobros",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobros_Deudas_DeudaId",
                table: "Cobros",
                column: "DeudaId",
                principalTable: "Deudas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deudas_Clientes_ClienteId",
                table: "Deudas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobros_Deudas_DeudaId",
                table: "Cobros");

            migrationBuilder.DropForeignKey(
                name: "FK_Deudas_Clientes_ClienteId",
                table: "Deudas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deudas",
                table: "Deudas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cobros",
                table: "Cobros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Deudas",
                newName: "Deuda");

            migrationBuilder.RenameTable(
                name: "Cobros",
                newName: "Cobro");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Deudas_ClienteId",
                table: "Deuda",
                newName: "IX_Deuda_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Cobros_DeudaId",
                table: "Cobro",
                newName: "IX_Cobro_DeudaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deuda",
                table: "Deuda",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cobro",
                table: "Cobro",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobro_Deuda_DeudaId",
                table: "Cobro",
                column: "DeudaId",
                principalTable: "Deuda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deuda_Cliente_ClienteId",
                table: "Deuda",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
