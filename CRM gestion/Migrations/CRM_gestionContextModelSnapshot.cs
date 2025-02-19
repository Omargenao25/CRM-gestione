﻿// <auto-generated />
using System;
using CRM_gestion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRM_gestion.Migrations
{
    [DbContext(typeof(CRM_gestionContext))]
    partial class CRM_gestionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRM_gestion.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("CRM_gestion.Models.Cobro", b =>
                {
                    b.Property<int>("CobroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CobroId"));

                    b.Property<int>("DeudaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCobro")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.HasKey("CobroId");

                    b.HasIndex("DeudaId");

                    b.ToTable("Cobros");
                });

            modelBuilder.Entity("CRM_gestion.Models.Deuda", b =>
                {
                    b.Property<int>("DeudaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeudaId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreación")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCobrado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.HasKey("DeudaId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Deudas");
                });

            modelBuilder.Entity("CRM_gestion.Models.Cobro", b =>
                {
                    b.HasOne("CRM_gestion.Models.Deuda", "Deuda")
                        .WithMany("Cobros")
                        .HasForeignKey("DeudaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deuda");
                });

            modelBuilder.Entity("CRM_gestion.Models.Deuda", b =>
                {
                    b.HasOne("CRM_gestion.Models.Cliente", "Cliente")
                        .WithMany("Deudas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("CRM_gestion.Models.Cliente", b =>
                {
                    b.Navigation("Deudas");
                });

            modelBuilder.Entity("CRM_gestion.Models.Deuda", b =>
                {
                    b.Navigation("Cobros");
                });
#pragma warning restore 612, 618
        }
    }
}
