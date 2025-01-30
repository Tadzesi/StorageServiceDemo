using StorageService.Api.Models;

namespace StorageService.Api.Interfaces
{
    public interface IDocumentFormatter
    {
        string ContentType { get; }
        string Format(Document document);
    }
}
