using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.OpenApi.Models;
using ProductSearchWithElasticsearch.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ElasticsearchClient>(_ =>
{

    var uri = new List<string>()
    {
        "http://127.0.0.1:2368"
    };
    var nodes = uri.Select(a => new Uri(a));

    var pool = new StaticNodePool(nodes);

    var settings = new ElasticsearchClientSettings(pool)
        .Authentication(new BasicAuthentication("userName", "password"))
        .DefaultIndex("product");

    return new ElasticsearchClient(settings);
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Elasticsearch Product Search API",
        Version = "v1",
        Description = "A simple API to demonstrate Elasticsearch integration with ASP.NET Core",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elasticsearch Product Search API V1");
        c.RoutePrefix = string.Empty;  
    });
}


app.MapPost("/products", async (Product product, ElasticsearchClient elasticClient) =>
{

    var response = await elasticClient.IndexAsync(product, idx => idx.Index("product").Id(product.Id));

    return response.IsValidResponse ? Results.Ok("Product indexed") : Results.BadRequest("Indexing failed");
});



app.MapGet("/products/search", async (string name, ElasticsearchClient elasticClient) =>
{
    var searchResponse = await elasticClient.SearchAsync<Product>(s => s
        .Index("product")
        .Query(q => q
            .Match(m => m
                .Field(f => f.Name)
                .Query(name)
            )
        )
    );

    return searchResponse.IsValidResponse ? Results.Ok(searchResponse.Documents) : Results.BadRequest("Search failed");
});

app.Run();
