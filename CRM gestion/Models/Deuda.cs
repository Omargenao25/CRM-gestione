using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_gestion.Models
{
    public class Deuda
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public Cliente Cliente { get; set; } // Relación muchos a 1
        public ICollection<Cobro> Cobros { get; set; } = new List<Cobro>(); // Relación 1 a muchos
    }
}
