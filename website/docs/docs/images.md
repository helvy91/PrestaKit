---
sidebar_position: 8
description: Images - using PrestaKit, a typed .NET client for the PrestaShop API.
keywords: [prestashop, .net, c#, api, client, webservice]
---

# ImageClient

Images are handled by `client.Images`. Its methods are generic over the resource
type that owns the image (products, categories, manufacturers, …), constrained to
entities that support images.

## Upload

```csharp
using var stream = File.OpenRead("chair.jpg");

long imageId = await client.Images.UploadAsync<Product>(
    productId,
    new ImageUpload { Content = stream, FileName = "chair.jpg" });
```

`ImageUpload` takes the image `Content` (a stream you own and dispose) and a
`FileName`. The content type is derived from the file name.

:::note

PrestaShop may re-encode or resize uploaded images, so the bytes you download
later may not be byte-for-byte identical to what you uploaded.

:::

## Download

```csharp
byte[] bytes = await client.Images.DownloadAsync<Product>(productId, imageId);
```

## List image ids

```csharp
var ids = await client.Images.ListIdsAsync<Product>(productId);
```

## Check and delete

```csharp
bool hasImage = await client.Images.HasImageAsync<Product>(productId);

await client.Images.DeleteAsync<Product>(productId, imageId);
```
