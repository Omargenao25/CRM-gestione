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

            // Relación entre Cliente y Deuda
            modelBuilder.Entity<Deuda>()
                .HasOne(d => d.Cliente)
                .WithMany(c => c.Deudas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Deuda y Cobro
            modelBuilder.Entity<Cobro>()
                .HasOne(c => c.Deuda)
                .WithMany(d => d.Cobros)
                .HasForeignKey(c => c.DeudaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración del tipo decimal (en caso de no usar el atributo en las propiedades)
            modelBuilder.Entity<Deuda>()
                .Property(d => d.Monto)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Cobro>()
                .Property(c => c.MontoCobrado)
                .HasColumnType("decimal(18,2)");
        }
    }
}
