using Microsoft.AspNetCore.Mvc;
using StorageService.Api.Interfaces;
using StorageService.Api.Models;
using StorageService.Api.Services;
using StorageService.Api.Storage;

namespace StorageService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentStorage _storage;
        private readonly DocumentFormatterFactory _formatterFactory;
        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(
            [FromKeyedServices(StorageType.InMemory)] IDocumentStorage storage,
            DocumentFormatterFactory formatterFactory,
            ILogger<DocumentsController> logger)
        {
            _storage = storage;
            _formatterFactory = formatterFactory;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(string id)
        {
            try
            {
                var document = await _storage.GetDocumentAsync(id);
                if (document == null)
                {
                    return NotFound();
                }

                var acceptHeader = Request.Headers.Accept.FirstOrDefault() ?? "application/json";
                var formatter = _formatterFactory.GetFormatter(acceptHeader);
                var formattedDocument = formatter.Format(document);

                return Content(formattedDocument, formatter.ContentType);
            }
            catch (NotSupportedException ex)
            {
                _logger.LogWarning(ex, "Unsupported format requested");
                return BadRequest("Requested format is not supported");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving document");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument(Document document)
        {
            try
            {
                await _storage.StoreDocumentAsync(document);
                return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Duplicate document ID");
                return Conflict("A document with this ID already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating document");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(string id, Document document)
        {
            try
            {
                if (id != document.Id)
                {
                    return BadRequest("Document ID in URL must match document ID in body");
                }

                await _storage.UpdateDocumentAsync(id, document);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating document");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
