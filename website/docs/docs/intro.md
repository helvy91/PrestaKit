---
sidebar_position: 1
title: Introduction
---

# PrestaKit

A modern, typed .NET client for the PrestaShop webservice API.

PrestaKit lets you automate your PrestaShop store from C#: upload products and
images, sync catalogs between shops, translate content with LLMs, power custom
dashboards, and generate reports — whatever your shop needs. All from a small,
modern NuGet package with zero runtime dependencies.

## Why PrestaKit

PrestaShop's webservice is powerful but rough to work with — XML everywhere,
stringly-typed data, and a long list of undocumented behaviors. PrestaKit wraps
it in a clean, strongly-typed client and quietly handles the rough edges:

- **Strongly typed** — `product.Price` is a `decimal`, translated fields are
  first-class, and your IDE autocompletes the whole API.
- **Handles the quirks** — empty fields, zero-dates, boolean `0`/`1`, read-only
  fields, and the product `state` trap are all handled for you.
- **Fluent, safe queries** — filter, sort, and paginate with expression-based,
  compile-checked field references.
- **Typed errors** — distinguish not-found, auth, and validation failures by
  exception type.
- **Zero runtime dependencies** in the core package.
- **Modern .NET** — targets .NET 8 and .NET 10.

## Supported versions

PrestaKit targets the **PrestaShop webservice API** and is tested against
**PrestaShop 8 and 9**.

Ready to start? Head to [Getting Started](./getting-started/installation).
