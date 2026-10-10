---
sidebar_position: 1
description: Installation - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

# Installation

PrestaKit ships as two packages.

### PrestaKit

The core library - the client, entities, query builder, and media clients. This
is all you need.

```bash
dotnet add package PrestaKit
```

### PrestaKit.Extensions.DependencyInjection

Optional. Adds `AddPrestaShopClient(...)` for registering the client with
`Microsoft.Extensions.DependencyInjection`. Use this if your app already uses the
.NET DI container (ASP.NET Core, worker services, etc.).

```bash
dotnet add package PrestaKit.Extensions.DependencyInjection
```

The core package has **no runtime dependencies**. The DI package depends only on
`Microsoft.Extensions.*`.

Both target **.NET 8** and **.NET 10**.

Next: [Set up the client](./setup).
