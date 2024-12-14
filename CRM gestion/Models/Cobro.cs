using System.ComponentModel.DataAnnotations;
using System;

namespace CRM_gestion.Models
{
    public class Cobro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DeudaId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public decimal MontoCobrado { get; set; }

        [Required]
        public DateTime FechaCobro { get; set; }

        public required Deuda Deuda { get; set; }
        public required Cliente Cliente { get; set; }
    }
}
