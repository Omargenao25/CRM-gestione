using System.ComponentModel.DataAnnotations;

namespace CRM_gestion.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public ICollection<Deuda> Deudas { get; set; } = new List<Deuda>(); // Relación 1 a muchos
    }
}