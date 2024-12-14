using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_gestion.Models
{
    public class Cobro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DeudaId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoCobrado { get; set; }

        [Required]
        public DateTime FechaCobro { get; set; }

        public required Deuda Deuda { get; set; }
       
    }
}
