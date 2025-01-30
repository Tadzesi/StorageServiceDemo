using StorageService.Api.Interfaces;
using StorageService.Api.Models;
using System.Collections.Concurrent;

namespace StorageService.Api.Storage
{
    public class InMemoryDocumentStorage : IDocumentStorage
    {
        private readonly ConcurrentDictionary<string, Document> _documents = new();

        public Task<Document> GetDocumentAsync(string id)
        {
            return _documents.TryGetValue(id, out var document)
                ? Task.FromResult(document)
                : Task.FromResult<Document>(null);
        }

        public Task StoreDocumentAsync(Document document)
        {
            if (!_documents.TryAdd(document.Id, document))
            {
                throw new InvalidOperationException($"Document with ID {document.Id} already exists.");
            }
            return Task.CompletedTask;
        }

        public Task UpdateDocumentAsync(string id, Document document)
        {
            _documents.AddOrUpdate(id, document, (_, _) => document);
            return Task.CompletedTask;
        }
    }
}
