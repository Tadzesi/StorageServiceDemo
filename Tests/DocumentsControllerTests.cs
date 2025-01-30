using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StorageService.Api.Controllers;
using StorageService.Api.Interfaces;
using StorageService.Api.Models;
using StorageService.Api.Services;

namespace Tests
{
    public class DocumentsControllerTests
    {
        private readonly Mock<IDocumentStorage> _mockStorage;
        private readonly Mock<ILogger<DocumentsController>> _mockLogger;
        private readonly DocumentFormatterFactory _formatterFactory;
        private readonly DocumentsController _controller;

        public DocumentsControllerTests()
        {
            _mockStorage = new Mock<IDocumentStorage>();
            _mockLogger = new Mock<ILogger<DocumentsController>>();

            var formatters = new List<IDocumentFormatter>
        {
            new JsonDocumentFormatterService(),
            new XmlDocumentFormatterService()
        };
            _formatterFactory = new DocumentFormatterFactory(formatters);

            _controller = new DocumentsController(_mockStorage.Object, _formatterFactory, _mockLogger.Object);

            // Setup default request headers
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Accept"] = "application/json";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public async Task GetDocument_WhenDocumentExists_ReturnsOkResult()
        {
            // Arrange
            var document = new Document
            {
                Id = "test-id",
                Tags = new List<string> { "test" },
                Data = new Dictionary<string, object> { { "key", "value" } }
            };

            _mockStorage.Setup(x => x.GetDocumentAsync("test-id"))
                .ReturnsAsync(document);

            // Act
            var result = await _controller.GetDocument("test-id");

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("application/json", contentResult.ContentType);
            Assert.Contains("test-id", contentResult.Content);
        }

        [Fact]
        public async Task GetDocument_WhenDocumentDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockStorage.Setup(x => x.GetDocumentAsync("non-existent"))
                .ReturnsAsync((Document)null);

            // Act
            var result = await _controller.GetDocument("non-existent");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateDocument_WithValidDocument_ReturnsCreatedResult()
        {
            // Arrange
            var document = new Document
            {
                Id = "new-id",
                Tags = new List<string> { "new" },
                Data = new Dictionary<string, object> { { "key", "value" } }
            };

            // Act
            var result = await _controller.CreateDocument(document);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(DocumentsController.GetDocument), createdResult.ActionName);
            Assert.Equal(document, createdResult.Value);
        }

        [Fact]
        public async Task CreateDocument_WhenDocumentExists_ReturnsConflict()
        {
            // Arrange
            var document = new Document
            {
                Id = "existing-id",
                Tags = new List<string> { "existing" },
                Data = new Dictionary<string, object> { { "key", "value" } }
            };

            _mockStorage.Setup(x => x.StoreDocumentAsync(document))
                .ThrowsAsync(new InvalidOperationException());

            // Act
            var result = await _controller.CreateDocument(document);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task UpdateDocument_WithValidDocument_ReturnsNoContent()
        {
            // Arrange
            var document = new Document
            {
                Id = "update-id",
                Tags = new List<string> { "update" },
                Data = new Dictionary<string, object> { { "key", "value" } }
            };

            // Act
            var result = await _controller.UpdateDocument("update-id", document);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateDocument_WithMismatchedIds_ReturnsBadRequest()
        {
            // Arrange
            var document = new Document
            {
                Id = "different-id",
                Tags = new List<string> { "update" },
                Data = new Dictionary<string, object> { { "key", "value" } }
            };

            // Act
            var result = await _controller.UpdateDocument("update-id", document);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
