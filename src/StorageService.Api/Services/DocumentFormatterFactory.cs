using StorageService.Api.Interfaces;

namespace StorageService.Api.Services
{
    public class DocumentFormatterFactory
    {
        private readonly IEnumerable<IDocumentFormatter> _formatters;

        public DocumentFormatterFactory(IEnumerable<IDocumentFormatter> formatters)
        {
            _formatters = formatters;
        }

        public IDocumentFormatter GetFormatter(string contentType)
        {
            return _formatters.FirstOrDefault(f => f.ContentType.Equals(contentType, StringComparison.OrdinalIgnoreCase))
                ?? throw new NotSupportedException($"Format {contentType} is not supported.");
        }
    }
}
