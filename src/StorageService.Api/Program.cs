using StorageService.Api.Interfaces;
using StorageService.Api.Services;
using StorageService.Api.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(); // This line requires the above using directives

builder.Services.AddSingleton<IDocumentStorage, InMemoryDocumentStorage>();
builder.Services.AddSingleton<IDocumentFormatter, JsonDocumentFormatterService>();
builder.Services.AddSingleton<IDocumentFormatter, XmlDocumentFormatterService>();
builder.Services.AddSingleton<DocumentFormatterFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
