
using StorageService.Api.Models;

namespace StorageService.Api.Interfaces
{

    public interface IDocumentStorage
    {
        Task<Document> GetDocumentAsync(string id);
        Task StoreDocumentAsync(Document document);
        Task UpdateDocumentAsync(string id, Document document);
    }
}
