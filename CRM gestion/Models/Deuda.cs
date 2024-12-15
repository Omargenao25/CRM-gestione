namespace CRM_gestion.Models
{
    public class Deuda
    {
        public int DeudaId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreación { get; set; }
        public DateTime FechaVencimiento { get; set; }

        // Relación cliente - deuda
        public int ClienteId { get; set; } // Clave Foránea
        public virtual Cliente? Cliente { get; set; } // Propiedad de navegación cliente

        // Cobros Asociados
        public virtual ICollection<Cobro>? Cobros { get; set; }
    }
}