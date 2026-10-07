---
sidebar_position: 3
---

# PrestaShopClient

`PrestaShopClient` (behind the `IPrestaShopClient` interface) is your entry point.
It exposes a typed client for each resource, plus clients for images and
attachment files.

## Resource accessors

Common resources have convenience properties:

```csharp
client.Products
client.Categories
client.Orders
client.Customers
client.StockAvailables
client.Combinations
client.Manufacturers
client.Suppliers
client.Addresses
client.OrderStates
```

Each returns an `IResourceClient<T>` for that entity — see
[Working with Resources](../resources/reading).

## Any resource

For a resource without a convenience property, use the generic accessor:

```csharp
var country = await client.Resource<Country>().GetAsync(1);
var zone    = await client.Resource<Zone>().ListAsync();
```

`Resource<T>()` works for any entity type, so you're never limited to the
convenience properties.

## Media clients

Two dedicated clients handle binary content:

```csharp
client.Images        // product/category/etc. images — see Media › Images
client.Attachments   // attachment files — see Media › Attachments
```

## Injecting the client

Depend on the interface, `IPrestaShopClient`, so your code stays testable:

```csharp
public class CatalogSync(IPrestaShopClient client)
{
    public async Task RunAsync()
    {
        var products = await client.Products.ListAsync();
        // ...
    }
}
```
