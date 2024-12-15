using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_gestion.Models
{
    public class Deuda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        public int ClienteId { get; set; } // Relación con el cliente

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de vencimiento.")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        // Colección de cobros asociados a la deuda
        public required Cliente Cliente { get; set; }
        public required ICollection<Cobro> Cobros { get; set; }
    }
}
