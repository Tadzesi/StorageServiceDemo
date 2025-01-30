using StorageService.Api.Interfaces;
using StorageService.Api.Models;
using System.Xml.Serialization;

namespace StorageService.Api.Services
{
    public class XmlDocumentFormatterService : IDocumentFormatter
    {
        public string ContentType => "application/xml";

        public string Format(Document document)
        {
            var serializer = new XmlSerializer(typeof(Document));
            using var writer = new StringWriter();
            serializer.Serialize(writer, document);
            return writer.ToString();
        }
    }
}
