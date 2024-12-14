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
        public CRM_gestionContext()
        {
        }

        public CRM_gestionContext (DbContextOptions<CRM_gestionContext> options)
            : base(options)
        {
        }

        public DbSet<CRM_gestion.Models.Cliente> Clientes { get; set; } = default!;
        public DbSet<CRM_gestion.Models.Cobro> Cobros { get; set; } = default!;
        public DbSet<CRM_gestion.Models.Deuda> Deudas { get; set; } = default!;

    }
}
