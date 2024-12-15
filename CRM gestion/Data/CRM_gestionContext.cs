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
            modelBuilder.Entity<Cliente>()
                        .HasKey(c => c.ClienteId);

            modelBuilder.Entity<Deuda>()
                        .HasKey(d => d.DeudaId);

            modelBuilder.Entity<Cobro>()
                        .HasKey(c => c.CobroId);

            modelBuilder.Entity<Deuda>()
                   .Property(d => d.Monto)
                   .HasColumnType("decimal(18,2)"); // Especificar precisión y escala

            modelBuilder.Entity<Cobro>()
                   .Property(c => c.Monto)
                   .HasColumnType("decimal(18,2)"); // Especificar precisión y escala

            // Relación explícita Cliente -> Deuda (uno a muchos)
            modelBuilder.Entity<Deuda>()
                .HasOne(d => d.Cliente) // Deuda tiene un Cliente
                .WithMany(c => c.Deudas) // Cliente tiene muchas Deudas
                .HasForeignKey(d => d.ClienteId) // La clave foránea en Deuda
                .OnDelete(DeleteBehavior.Cascade); // Comportamiento de eliminación

            // Relación explícita Deuda -> Cobros (uno a muchos)
            modelBuilder.Entity<Deuda>()
                .HasMany(d => d.Cobros) // Deuda tiene muchos Cobros
                .WithOne(c => c.Deuda) // Cobro está relacionado con una Deuda
                .HasForeignKey(c => c.DeudaId) // Clave foránea en Cobro
                .OnDelete(DeleteBehavior.Cascade); // Comportamiento de eliminación

            base.OnModelCreating(modelBuilder);
        }
    }
}
