---
sidebar_position: 3
description: First Request - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

# Creating a request

With a configured `client`, fetching a product is one line:

```csharp
var product = await client.Products.GetAsync(1);

Console.WriteLine(product.Name);     // translated field — reads the default language
Console.WriteLine(product.Price);    // decimal?
Console.WriteLine(product.Active);   // bool?
```

Create a product:

```csharp
var created = await client.Products.AddAsync(new Product
{
    Name = "Wooden Chair",
    Price = 49.99m,
    Active = true,
    IdCategoryDefault = 2,
    Associations = new ProductAssociations { Categories = { new EntityRef(2) } },
});

Console.WriteLine($"Created product {created.Id}");
```

List active products with a filter:

```csharp
var query = new Query<Product>();
query.WhereEquals(p => p.Active, "1");

var products = await client.Products.ListAsync(query);
```

That's the shape of everything in PrestaKit: a typed client, typed entities, and
methods that return real C# objects. The rest of these docs cover each area in
depth.
