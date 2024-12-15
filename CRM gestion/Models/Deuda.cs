using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_gestion.Models
{
    public class Deuda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        public ICollection<Cobro> Cobros { get; set; }
    }

}
