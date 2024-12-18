using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; } // Clave primaría
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Navegación a las deudas asociadas
        public virtual ICollection<Deuda>? Deudas { get; set; } = new List<Deuda>(); // Relación 1 a muchos
    }
}