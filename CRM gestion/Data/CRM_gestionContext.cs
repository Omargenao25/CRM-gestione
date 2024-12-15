using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM_gestion.Models;

namespace CRM_gestion.Data
{
    public class CRM_gestionContext : DbContext
    {
        public CRM_gestionContext(DbContextOptions<CRM_gestionContext> options)
            : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Cobro> Cobros { get; set; } = default!;
        public DbSet<Deuda> Deudas { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre Cliente y Deuda
            modelBuilder.Entity<CRM_gestion.Models.Deuda>()
                .HasOne(d => d.Cliente) // Una Deuda tiene un Cliente
                .WithMany(c => c.Deudas) // Un Cliente puede tener muchas Deudas
                .HasForeignKey(d => d.ClienteId) // La clave foránea de Deuda será ClienteId
                .OnDelete(DeleteBehavior.Cascade); // Eliminar deudas si se elimina el cliente

            // Configuración de la relación entre Deuda y Cobro
            modelBuilder.Entity<CRM_gestion.Models.Cobro>()
                .HasOne(c => c.Deuda) // Un Cobro tiene una Deuda
                .WithMany(d => d.Cobros) // Una Deuda puede tener muchos Cobros
                .HasForeignKey(c => c.DeudaId) // La clave foránea de Cobro será DeudaId
                .OnDelete(DeleteBehavior.Cascade); // Eliminar cobros si se elimina la deuda

            // Configuración de tipos de datos
            modelBuilder.Entity<CRM_gestion.Models.Deuda>()
                .Property(d => d.Monto)
                .HasColumnType("decimal(18,2)"); // Definir el tipo de columna para el monto

            modelBuilder.Entity<CRM_gestion.Models.Cobro>()
                .Property(c => c.MontoCobrado)
                .HasColumnType("decimal(18,2)"); // Definir el tipo de columna para el monto cobrado
        }
    }

}
