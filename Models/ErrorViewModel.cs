namespace InventarioLPS.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public string? Message { get; set; }
        public string? InnerMessage { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
