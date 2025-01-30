using StorageService.Api.Interfaces;
using StorageService.Api.Models;

namespace StorageService.Api.Storage
{
    public class DatabaseDocumentStorage : IDocumentStorage
    {
        //Todo: ToDo: DbContext should be imlemented
        //private readonly DocumentDbContext _context;
        private readonly ILogger<DatabaseDocumentStorage> _logger;

        public DatabaseDocumentStorage(
            // Todo: Implement DbContext
            //DocumentDbContext context,
            ILogger<DatabaseDocumentStorage> logger)
        {
            //ToDo: DbContext should be imlemented
            //_context = context;
            _logger = logger;
        }

        public async Task<Document> GetDocumentAsync(string id)
        {
            try
            {
                // TODO: Implement database retrieval
                // 1. Query DocumentEntity by id
                // 2. Convert Tags and Data from JSON strings to objects
                // 3. Map to Document model
                // 4. Return result
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving document {Id} from database", id);
                throw;
            }
        }

        public async Task StoreDocumentAsync(Document document)
        {
            try
            {
                // TODO: Implement database storage
                // 1. Check if document already exists
                // 2. Convert Tags and Data to JSON strings
                // 3. Create new DocumentEntity
                // 4. Add to context and save
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error storing document {Id} in database", document.Id);
                throw;
            }
        }

        public async Task UpdateDocumentAsync(string id, Document document)
        {
            try
            {
                // TODO: Implement database update
                // 1. Check if document exists
                // 2. Convert Tags and Data to JSON strings
                // 3. Update existing DocumentEntity
                // 4. Update UpdatedAt timestamp
                // 5. Save changes
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating document {Id} in database", id);
                throw;
            }
        }
    }
}
