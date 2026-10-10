---
sidebar_position: 1
description: Reading - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

# Reading

Every resource client (`client.Products`, `client.Resource<T>()`, …) exposes the
same read methods.

## Get by id

`GetAsync` returns the resource, or throws `PrestaShopNotFoundException` if it
doesn't exist:

```csharp
var product = await client.Products.GetAsync(1);
```

## Find by id

`FindAsync` returns `null` instead of throwing when the resource is missing — use
it when "not found" is an expected, non-exceptional outcome:

```csharp
var product = await client.Products.FindAsync(999);
if (product is null)
{
    // doesn't exist — handle it
}
```

:::tip 

Use `GetAsync` when the resource *should* exist (a missing one is a real error).
Use `FindAsync` when you're checking whether it exists. `GetAsync` throws;
`FindAsync` returns `null`. Both throw for auth and server errors.

:::

## Check existence

`ExistsAsync` does a lightweight `HEAD` request and returns a `bool` without
transferring the entity body:

```csharp
if (await client.Products.ExistsAsync(1))
{
    // ...
}
```

See [Listing](./listing) for fetching many resources, and
[Querying](../querying/query-builder) for filtering.
