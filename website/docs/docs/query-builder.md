---
sidebar_position: 5
---

# `Query<T>`

`Query<T>` builds a filtered, sorted, paged request with expression-based field
references — no magic strings, and the field names are checked at compile time.

Create one with `Query<T>.Create()` and chain the methods you need:

```csharp
var query = new Query<Product>()
    .WhereEquals(p => p.Active, "1")
    .WhereBeginsWith(p => p.Reference, "ABC")
    .OrderBy(p => p.Id, desc: true)
    .Page(0, 50);

var results = await client.Products.ListAsync(query);
```

## Filters

```csharp
// exact match
query.WhereEquals(p => p.Active, "1");

// begins with (prefix)
query.WhereBeginsWith(p => p.Reference, "ABC");

// inclusive range
query.WhereBetween(p => p.Price, "10", "50");
```

Field references are expressions (`p => p.Active`), so a typo or renamed property
is a compile error, not a silent runtime failure. PrestaKit resolves each
expression to the correct PrestaShop field name for you.

## Ordering

```csharp
query.OrderBy(p => p.Id);              // ascending
query.OrderBy(p => p.Id, desc: true);  // descending
```

## Paging

```csharp
query.Page(skip: 0, take: 50);   // first 50
query.Page(skip: 50, take: 50);  // next 50
```

## Reusing a query

A query is a plain object you can build up and pass to any matching list call:

```csharp
var active = new Query<Product>().WhereEquals(p => p.Active, "1");

var list = await client.Products.ListAsync(active);
var ids  = await client.Products.ListIdsAsync(active);
```

For streaming large result sets, see [EnumerateAsync](./enumerate).
