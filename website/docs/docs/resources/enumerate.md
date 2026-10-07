---
sidebar_position: 4
---

# EnumerateAsync for Pagination

`ListAsync()` loads every matching resource into memory at once. On a real shop
with thousands of products or orders, that's wasteful and slow. `EnumerateAsync`
streams the results instead - it fetches one page at a time and yields each
resource as you go, so only a single page is ever in memory.

```csharp
await foreach (var product in client.Products.EnumerateAsync(
    new Query<Product>(), pageSize: 100))
{
    // process one product at a time
    Console.WriteLine(product.Name);
}
```

It returns an `IAsyncEnumerable<T>`, so it works with `await foreach`, LINQ's
async operators, and cancellation.

## Page size

The `pageSize` argument controls how many resources are fetched per request
(default 50). Larger pages mean fewer requests but more memory per page; smaller
pages mean the opposite. A few hundred is usually a good balance.

## When to use it

- **`EnumerateAsync`** — large result sets, or when you process items one by one
  and don't need them all in memory.
- **`ListAsync(query)`** — small result sets you want materialized as a list.

:::tip

Because paging is position-based, order by a stable key such as the id
(`OrderBy(p => p.Id)`) when enumerating. If the underlying data changes while you
page through it, an unstable order can skip or repeat items.

:::
