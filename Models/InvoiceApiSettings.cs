namespace InvoiceApi.Models
{
    public class InvoiceApiSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string InvoiceCollectionName { get; set; } = null!;
    }
}
