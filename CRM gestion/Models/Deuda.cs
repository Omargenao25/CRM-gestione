using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Deuda
    {
        public int DeudaId { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime FechaCreación { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total cobrado no puede ser negativo.")]
        public decimal TotalCobrado { get; set; } // Nueva Columna

        // Relación cliente - deuda
        [Required(ErrorMessage = "El cliente asociado es obligatorio.")]
        public int ClienteId { get; set; } // Clave Foránea

        public virtual Cliente? Cliente { get; set; } // Propiedad de navegación cliente

        // Cobros Asociados
        public virtual ICollection<Cobro>? Cobros { get; set; }
    }
}