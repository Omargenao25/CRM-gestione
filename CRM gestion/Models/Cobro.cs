namespace CRM_gestion.Models
{
    public class Cobro
    {
        public int CobroId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCobro { get; set; }

        // Relacion Deuda - cobros
        public int DeudaId { get; set; }
        public virtual Deuda? Deuda { get; set; }
    }
}