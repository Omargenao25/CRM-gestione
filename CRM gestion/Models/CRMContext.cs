using Microsoft.EntityFrameworkCore;

namespace CRM_gestion.Models
{
   
        public class CRMContext : DbContext
        {
            public CRMContext(DbContextOptions<CRMContext> options)
                : base(options)
            {
            }

            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Deuda> Deudas { get; set; }
        }
}
