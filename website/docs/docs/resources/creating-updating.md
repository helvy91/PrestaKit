---
sidebar_position: 3
---

# Create/Update/Delete

## Creating

`AddAsync` creates a resource and returns it with the server-assigned id:

```csharp
var created = await client.Products.AddAsync(new Product
{
    Name = "Wooden Chair",
    Price = 49.99m,
    Active = true,
    IdCategoryDefault = 2,
    Associations = new ProductAssociations { Categories = { new EntityRef(2) } },
});

Console.WriteLine(created.Id);   // assigned by PrestaShop
```

PrestaKit strips read-only and server-managed fields from the request
automatically, so you don't have to clear them yourself.

:::note

PrestaShop validates required fields on create and rejects the request with a
`PrestaShopApiException` (and the reason in `Errors`) if one is missing. Which
fields are required depends on the resource — for example a product needs a
price and a default category.

:::

## Updating

`UpdateAsync` sends a modified entity back. The entity's `Id` must be set:

```csharp
var product = await client.Products.GetAsync(1);

product.Price = 44.99m;
await client.Products.UpdateAsync(product);
```

The common pattern is read-modify-write: fetch the entity, change what you need,
send it back. As with create, read-only fields are handled for you.

## Deleting

Delete by id, or by passing the entity (its `Id` must be set):

```csharp
await client.Products.DeleteAsync(created.Id!.Value);
// or
await client.Products.DeleteAsync(created);
```
