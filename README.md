# PrestaKit

A modern, typed .NET client for the PrestaShop webservice API. Easy to use,
strongly typed, and kept up to date — available for .NET 8 and .NET 10.

## Supported versions

Tested against **PrestaShop 8 and 9** (the webservice API). Most of the
installed base runs these; older 1.x versions are not a supported target.

## Installation

```
dotnet add package PrestaKit
```

For dependency injection support:

```
dotnet add package PrestaKit.Extensions.DependencyInjection
```

## Quick start

### With dependency injection (recommended)

```csharp
using Microsoft.Extensions.DependencyInjection;

services.AddPrestaShopClient(options =>
{
    options.BaseUrl = new Uri("https://your-shop.com/api/");
    options.ApiKey  = "YOUR_WEBSERVICE_KEY";
});
```

```csharp
public class ProductService(IPrestaShopClient client)
{
    public async Task<Product> GetProductAsync(long id)
        => await client.Products.GetAsync(id);
}
```

### Without DI

```csharp
using PrestaKit;

var http = new HttpClient { BaseAddress = new Uri("https://your-shop.com/api/") };
// authenticate with the API key (Basic auth: key as username, empty password)
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("YOUR_WEBSERVICE_KEY:")));

var client = new PrestaShopClient(http);
```

## Usage

### Reading

```csharp
// get one
var product = await client.Products.GetAsync(1);

// get one, or null if it doesn't exist (no exception)
var maybe = await client.Products.FindAsync(999);

// check existence cheaply (HEAD request)
bool exists = await client.Products.ExistsAsync(1);

// list all (careful on large catalogs — see EnumerateAsync)
var all = await client.Products.ListAsync();
```

### Querying

```csharp
var query = Query<Product>.Create()
    .WhereEquals(p => p.Active, "1")
    .WhereBeginsWith(p => p.Reference, "ABC")
    .OrderBy(p => p.Id, desc: true)
    .Page(skip: 0, take: 50);

var results = await client.Products.ListAsync(query);
```

### Paginating large resources

`EnumerateAsync` streams pages so you never hold an entire catalog in memory:

```csharp
await foreach (var product in client.Products.EnumerateAsync(
    Query<Product>.Create(), pageSize: 100))
{
    // process one product at a time; only one page is in flight
}
```

### Creating and updating

```csharp
var created = await client.Products.AddAsync(new Product
{
    Name = "Wooden Chair",       // implicitly the default language
    Price = 49.99m,
    Active = true,
    IdCategoryDefault = 2,
    Associations = new ProductAssociations { Categories = { new EntityRef(2) } },
});

created.Price = 44.99m;
await client.Products.UpdateAsync(created);
```

### Deleting

```csharp
await client.Products.DeleteAsync(created.Id!.Value);
```

### Any resource

Convenience properties exist for the common resources (`Products`, `Categories`,
`Orders`, `Customers`, `StockAvailables`, and more). For anything else, use the
generic accessor:

```csharp
var country = await client.Resource<Country>().GetAsync(1);
```

### Translated fields

Multi-language fields are strongly typed:

```csharp
product.Name = "Chair";              // sets the default language
string name = product.Name;          // reads the default language
product.Name[2] = "Krzesło";         // set a specific language by id
var polish = product.Name[2];        // read a specific language
```

### Stock

Stock is managed through the `StockAvailable` resource (not the product
directly), following PrestaShop's model:

```csharp
var product = await client.Products.GetAsync(1);
var stockId = product.Associations!.StockAvailables[0].Id;

var stock = await client.Resource<StockAvailable>().GetAsync(stockId);
stock.Quantity = 100;
await client.Resource<StockAvailable>().UpdateAsync(stock);
```

### Images and attachments

```csharp
// upload a product image
using var stream = File.OpenRead("chair.jpg");
long imageId = await client.Images.UploadAsync<Product>(productId,
    new ImageUpload { Content = stream, FileName = "chair.jpg" });

// download it back
byte[] bytes = await client.Images.DownloadAsync<Product>(productId, imageId);
```

## Error handling

Failures surface as typed exceptions:

```csharp
try
{
    var product = await client.Products.GetAsync(id);
}
catch (PrestaShopNotFoundException)
{
    // 404 — resource doesn't exist
}
catch (PrestaShopAuthException)
{
    // 401/403 — bad or unauthorized API key
}
catch (PrestaShopApiException ex)
{
    // any other API failure — inspect ex.StatusCode and ex.Errors
    foreach (var error in ex.Errors)
        Console.WriteLine($"[{error.Code}] {error.Message}");
}
```

`PrestaShopSerializationException` is thrown if a response can't be parsed.
Transport failures (`HttpRequestException`, `OperationCanceledException`)
propagate unwrapped.

## Known limitations

- **Attachment upload** is affected by an open PrestaShop core bug
  ([#35922](https://github.com/PrestaShop/PrestaShop/issues/35922)) where the
  file uploads successfully but the response can be malformed. Behavior is best
  against production-configured shops.
- The PrestaShop 9 **Admin API** (OAuth2/JSON, CQRS) is a separate API from the
  webservice PrestaKit targets. Support for it may arrive as a separate package.

## License

MIT
