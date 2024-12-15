using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_gestion.Models
{
    public class Cobro
    {
        public int Id { get; set; }
        public int DeudaId { get; set; }
        public decimal MontoCobrado { get; set; }
        public DateTime FechaCobro { get; set; }

        public Deuda Deuda { get; set; } // Relación muchos a 1
    }
}
