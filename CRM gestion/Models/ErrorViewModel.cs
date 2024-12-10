namespace CRM_gestion.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

      

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
