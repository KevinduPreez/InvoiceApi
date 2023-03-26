using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace InvoiceApi.Models
{
    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("Name")]
        public string InvoiceName { get; set; }
        public decimal InvoiceRefNumber { get; set; }
        public string Contact { get; set; }
        public string Amount { get; set; }
    }
}
