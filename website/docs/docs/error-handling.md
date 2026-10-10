---
sidebar_position: 6
description: Error Handling - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

# Exceptions

PrestaKit turns PrestaShop's API failures into typed exceptions you can catch by
category.

## The exception hierarchy

```
PrestaShopException                  (base — catch this for anything PrestaKit throws)
├── PrestaShopApiException           (the API rejected the request)
│   ├── PrestaShopNotFoundException  (404)
│   └── PrestaShopAuthException      (401 / 403)
└── PrestaShopSerializationException (a response couldn't be parsed)
```

## Catching by type

```csharp
try
{
    var product = await client.Products.GetAsync(id);
}
catch (PrestaShopNotFoundException)
{
    // 404 — the resource doesn't exist
}
catch (PrestaShopAuthException)
{
    // 401 / 403 — bad or unauthorized API key
}
catch (PrestaShopApiException ex)
{
    // any other API failure, e.g. a validation error on create/update
    foreach (var error in ex.Errors)
        Console.WriteLine($"[{error.Code}] {error.Message}");
}
```

## Exception details

`PrestaShopApiException` carries everything you need to log or react:

| Property         | Description                                      |
|------------------|--------------------------------------------------|
| `HttpStatusCode` | The HTTP status returned by PrestaShop.          |
| `Errors`         | The parsed PrestaShop errors (code + message).   |
| `ResponseBody`   | The raw response body, when available.           |
| `RequestUri`     | The request that failed, when available.         |

`PrestaShopSerializationException` carries the raw payload in `XmlContent` so you
can see exactly what failed to parse.

## What isn't wrapped

Transport-level failures (`HttpRequestException`) and cancellation
(`OperationCanceledException`) propagate unwrapped, so you can handle connectivity
and timeouts the usual way.
