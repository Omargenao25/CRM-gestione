using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Cobro
    {
        public int CobroId { get; set; }

        [Required(ErrorMessage = "El monto del cobro es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto del cobro debe ser mayor a cero.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha del cobro es obligatoria.")]
        public DateTime FechaCobro { get; set; }

        // Relacion Deuda - cobros
        [Required(ErrorMessage = "La deuda asociada es obligatoria.")]
        public int DeudaId { get; set; }

        public virtual Deuda? Deuda { get; set; }
    }
}