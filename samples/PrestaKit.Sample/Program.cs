using PrestaKit;
using PrestaKit.Entities;
using PrestaKit.Entities.Associations;
using PrestaKit.Querying;
using System.Net.Http.Headers;
using System.Text;

// PrestaKit Sample
//
// Demonstrates common operations: create, read, query, update, delete a product.
//
// To run: set your shop URL and API key below, then `dotnet run`.
// Note: this creates and deletes a real product — use a development shop.

// --- Configure ---
var http = new HttpClient { BaseAddress = new Uri("https://your-shop.com/api/") };
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("YOUR_API_KEY:")));
var client = new PrestaShopClient(http);

// --- Create a product ---
var product = await client.Products.AddAsync(new Product
{
    Name = "Sample Chair",
    Price = 49.99m,
    Active = true,
    IdCategoryDefault = 2,
    Associations = new ProductAssociations { Categories = [2] },
});
Console.WriteLine($"Created product {product.Id}");

// --- Read it back ---
var fetched = await client.Products.GetAsync(product.Id!.Value);
Console.WriteLine($"Fetched: {fetched.Name} — {fetched.Price:C}");

// --- Query ---
var active = await client.Products.ListAsync(
    new Query<Product>().WhereEquals(p => p.Active, "1").Page(0, 10));
Console.WriteLine($"Found {active.Count} active products");

// --- Update ---
fetched.Price = 44.99m;
await client.Products.UpdateAsync(fetched);
Console.WriteLine("Updated price");

// --- Clean up ---
await client.Products.DeleteAsync(product.Id.Value);
Console.WriteLine("Deleted");