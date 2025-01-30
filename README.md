# Document Storage API

This ASP.NET Core Web API provides document storage capabilities with support for multiple formats (JSON, XML) and flexible storage options.

## Features

- Store and retrieve documents with unique identifiers
- Support for multiple document formats (JSON, XML)
- Extensible architecture for adding new formats
- Configurable storage backends
- High-performance design for read-heavy workloads
- Comprehensive test coverage

## Prerequisites

- .NET 7.0 SDK or later
- Visual Studio 2022 or VS Code
- Git (optional)

## Installation

1. Clone the repository (if using Git):
```bash
git clone [repository-url]
cd DocumentStorageAPI
```

2. Install required NuGet packages:
```bash
dotnet restore
```

3. Install Swagger package:
```bash
dotget add package Swashbuckle.AspNetCore
```

## Running the Application

### Using Visual Studio:
1. Open the solution file (.sln) in Visual Studio
2. Press F5 or click the "Debug" button
3. Your default browser will open automatically
4. Add "/swagger" to the URL to access the Swagger UI
   Example: `https://localhost:7000/swagger`

### Using Command Line:
1. Navigate to the project directory
2. Run the following command:
```bash
dotnet run
```
3. Open a browser and navigate to:
   - Swagger UI: `https://localhost:5001/swagger`
   (Note: Port number may vary, check the console output for the correct port)

## Using the API via Swagger

1. Open the Swagger UI at `https://localhost:<port>/swagger`
2. You'll see three available endpoints:
   - POST /documents - Create a new document
   - GET /documents/{id} - Retrieve a document
   - PUT /documents/{id} - Update an existing document

3. To create a document:
   - Click on POST /documents
   - Click "Try it out"
   - Enter a JSON request body like this:
```json
{
  "id": "doc-1",
  "tags": ["important", ".net"],
  "data": {
    "some": "data",
    "optional": "fields"
  }
}
```
   - Click "Execute"

4. To retrieve a document:
   - Click on GET /documents/{id}
   - Click "Try it out"
   - Enter the document ID
   - Optionally set the Accept header to `application/xml` or `application/json`
   - Click "Execute"

## Configuration

The application uses the following default configuration:
- In-memory document storage
- JSON and XML format support
- Swagger UI enabled in development mode
- HTTPS redirection enabled

## Adding New Format Support

1. Create a new formatter class implementing `IDocumentFormatter`:
```csharp
public class NewFormatter : IDocumentFormatter
{
    public string ContentType => "application/new-format";
    
    public string Format(Document document)
    {
        // Implementation
    }
}
```

2. Register the formatter in Program.cs:
```csharp
builder.Services.AddSingleton<IDocumentFormatter, NewFormatter>();
```

## Running Tests

```bash
dotnet test
```

## Common Issues

1. **Swagger not loading**: 
   - Verify the correct URL with /swagger at the end
   - Check if Swashbuckle.AspNetCore package is installed
   - Ensure Swagger is configured in Program.cs

2. **Port already in use**:
   - Change the port in launchSettings.json
   - Stop other running applications that might be using the port

## Support

For issues or questions, please create an issue in the repository or contact the development team.
