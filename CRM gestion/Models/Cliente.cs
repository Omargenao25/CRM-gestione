using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; } // Clave primaria

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        [StringLength(15, ErrorMessage = "El número de teléfono no puede tener más de 15 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        // Navegación a las deudas asociadas
        public virtual ICollection<Deuda>? Deudas { get; set; } = new List<Deuda>(); // Relación 1 a muchos
    }
}