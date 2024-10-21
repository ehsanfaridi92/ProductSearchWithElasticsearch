
# Elasticsearch Product Search API

A simple API built with ASP.NET Core that demonstrates integration with Elasticsearch for indexing and searching products.

### ⭐ Give a Star!
If you find this repository valuable, please consider giving it a star to show your support!


## Features
- **Index products**: Add products to Elasticsearch index.
- **Search products**: Search products by name using Elasticsearch.
- **Swagger Integration**: Easily test and explore the API using Swagger UI.

## Prerequisites

Before running the application, ensure you have the following:

- .NET 6 SDK installed on your machine.
- Elasticsearch instance running locally or on a remote server.
- Basic understanding of how Elasticsearch works.

## Getting Started

1. Clone this repository:

   ```bash
   git clone https://github.com/yourusername/ProductSearchWithElasticsearch.git
   cd ProductSearchWithElasticsearch
   ```

2. Configure the `Elasticsearch` connection settings in the code. Adjust the following values in the code to point to your running Elasticsearch instance:

   ```csharp
   var uri = new List<string>()
   {
       "http://127.0.0.1:2367" // Change this to your Elasticsearch server URL
   };
   var settings = new ElasticsearchClientSettings(pool)
       .Authentication(new BasicAuthentication("elastic", "yourpassword")) // Add your credentials
       .DefaultIndex("product");
   ```

3. Run the application:

   ```bash
   dotnet run
   ```

4. Once the application is running, open your browser and navigate to `http://localhost:5024/index.html` to explore the API using Swagger UI.

## API Endpoints

### 1. Index a Product (POST)

Endpoint: `/products`

Description: Index a new product into Elasticsearch.

#### Request body:

```json
{
  "id": "1",
  "name": "Laptop",
  "description": "A high-performance laptop.",
  "price": 1500.00
}
```

### 2. Search for Products by Name (GET)

Endpoint: `/products/search`

Description: Search for products based on their name.

#### Query Parameter:

- `name`: The name of the product to search for.

Example:

```bash
GET /products/search?name=Laptop
```

## Dependencies

- [Elasticsearch](https://www.elastic.co/elasticsearch/): Open source, distributed, RESTful search engine.
- [Elastic.Clients.Elasticsearch](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch/): Elasticsearch client for .NET.
- [Swagger](https://swagger.io/): API documentation tool.

## License

