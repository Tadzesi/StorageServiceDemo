using StorageService.Api.Interfaces;
using StorageService.Api.Models;
using System.Text.Json;

namespace StorageService.Api.Services
{
    public class JsonDocumentFormatterService : IDocumentFormatter
    {
        public string ContentType => "application/json";

        public string Format(Document document)
        {
            return JsonSerializer.Serialize(document);
        }
    }
}
