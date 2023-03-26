using Microsoft.Extensions.Options;
using MongoDB.Driver;
using InvoiceApi.Models;


namespace InvoiceApi.Services
{
    public class InvoiceService
    {
        private readonly IMongoCollection<Invoice> _invoiceCollection;

        public InvoiceService(
        IOptions<InvoiceApiSettings> InvoiceDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                InvoiceDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                InvoiceDatabaseSettings.Value.DatabaseName);

            _invoiceCollection = mongoDatabase.GetCollection<Invoice>(
                InvoiceDatabaseSettings.Value.InvoiceCollectionName);
        }

        public async Task<List<Invoice>> GetAsync() => await _invoiceCollection.Find(_ => true).ToListAsync();
        public async Task<Invoice?> GetAsync(string id) =>
        await _invoiceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Invoice newInvoice) =>
            await _invoiceCollection.InsertOneAsync(newInvoice);

        public async Task UpdateAsync(string id, Invoice updatednewInvoice) =>
            await _invoiceCollection.ReplaceOneAsync(x => x.Id == id, updatednewInvoice);

        public async Task RemoveAsync(string id) =>
            await _invoiceCollection.DeleteOneAsync(x => x.Id == id);
    }
}
