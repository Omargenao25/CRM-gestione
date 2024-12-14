using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public required string Telefono { get; set; }

        // Colección de deudas asociadas al cliente
        public ICollection<Deuda> Deudas { get; set; } = new List<Deuda>();

    }
}