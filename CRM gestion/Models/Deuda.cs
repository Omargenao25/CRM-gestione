using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Deuda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        public int ClienteId { get; set; } // Relación con el cliente

        [Required(ErrorMessage = "Debe ingresar el monto.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de vencimiento.")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        // Propiedad de navegación para la relación con Cliente
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        public virtual Cliente Cliente { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
    }
}
