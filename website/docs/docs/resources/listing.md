---
sidebar_position: 2
description: Listing - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

# Listing

There are three ways to fetch multiple resources, depending on what you need.

## List all

`ListAsync()` with no arguments returns every resource of that type:

```csharp
var all = await client.Products.ListAsync();
```

:::warning

`ListAsync()` fetches everything in a single request. For large resources:
products, orders, customers on a real shop - prefer
[`EnumerateAsync()`](../querying/enumerate), which pages the results so you never
load the whole set into memory at once.

:::

## List with a query

Pass a [`Query<T>`](../querying/query-builder) to filter, sort, and page:

```csharp
var results = await client.Products.ListAsync(
    new Query<Product>()
        .WhereEquals(p => p.Active, "1")
        .OrderBy(p => p.Id, desc: true)
        .Page(0, 50));
```

An empty result is a valid empty list, not an error.

## List ids only

When you only need the ids (not the full entities), `ListIdsAsync` is cheaper —
it asks PrestaShop for ids alone:

```csharp
var ids = await client.Products.ListIdsAsync(
    new Query<Product>().WhereEquals(p => p.Active, "1"));
// ids is IReadOnlyList<long>
```

## Which to use

| Method                | Returns                | Use when                               |
|-----------------------|------------------------|----------------------------------------|
| `ListAsync()`         | all entities           | small resources, you want everything   |
| `ListAsync(query)`    | filtered entities      | you need specific resources            |
| `ListIdsAsync(query)` | ids only               | you only need ids                      |
| `EnumerateAsync(...)` | a streamed sequence    | large resources, process page by page  |
