namespace CRM_gestion.Models.ViewModels
{
    public class DeudasViewModel
    {
        public List<Deuda>? Deudas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
