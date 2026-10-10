---
sidebar_position: 2
description: Setup - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

# Setup

To talk to a shop you need two things: the shop's API base URL and a webservice
API key. Both go into the client when you create it.

## Getting an API key

In the PrestaShop back office:

1. Go to **Advanced Parameters → Webservice** and enable the webservice.
2. Add a new key. PrestaShop generates the key string for you.
3. Grant the key permissions for the resources you'll use (products, categories,
   stock, and so on) — and the HTTP methods you need (GET, POST, PUT, DELETE).
4. Make sure the key is associated with your shop.

Your base URL is your shop URL followed by `/api/`, for example
`https://your-shop.com/api/`.

:::note 

The webservice uses HTTP Basic auth, with the **API key as the username** and an
**empty password**. PrestaKit handles this for you, you just supply the key.

:::

## Creating the client

<Tabs groupId="setup">
  <TabItem value="di" label="Dependency Injection" default>

Register the client once at startup:

```csharp
using Microsoft.Extensions.DependencyInjection;

builder.Services.AddPrestaShopClient(options =>
{
    options.BaseUrl = new Uri("https://your-shop.com/api/");
    options.ApiKey  = "YOUR_WEBSERVICE_KEY";
});
```

Then inject `IPrestaShopClient` anywhere:

```csharp
public class ProductService(IPrestaShopClient client)
{
    public Task<Product> GetAsync(long id) => client.Products.GetAsync(id);
}
```

The DI registration configures the underlying `HttpClient` (base address and
authentication) for you, and its lifetime is managed by the container.

  </TabItem>
  <TabItem value="manual" label="Manual">

Create the client directly. You supply an `HttpClient` with the base address and
authentication header set:

```csharp
using System.Net.Http.Headers;
using System.Text;
using PrestaKit;

var http = new HttpClient { BaseAddress = new Uri("https://your-shop.com/api/") };
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("YOUR_WEBSERVICE_KEY:")));

var client = new PrestaShopClient(http);
```

:::tip 

In long-running applications, prefer the dependency-injection setup (or supply an
`HttpClient` whose lifetime you manage) rather than creating a new `HttpClient`
per call. The manual constructor is convenient for scripts and short-lived
processes.

:::

  </TabItem>
</Tabs>

## Options

`PrestaShopClientOptions` has two properties:

| Property   | Type  | Description                                              |
|------------|-------|---------------------------------------------------------|
| `BaseUrl`  | `Uri` | The shop's API base URL, e.g. `https://shop.com/api/`.   |
| `ApiKey`   | `string` | The webservice API key.                              |

Next: [Make your first request](./first-request).
