---
sidebar_position: 9
---

# AttachmentFileClient

Attachment files are handled by `client.Attachments`. Unlike images, attachments
are standalone resources, so the methods aren't generic.

## Upload

```csharp
using var stream = File.OpenRead("manual.pdf");

var attachment = await client.Attachments.UploadAsync(
    new AttachmentUpload { Content = stream, FileName = "manual.pdf" });

Console.WriteLine(attachment.Id);
```

`UploadAsync` returns the created `Attachment`. PrestaShop stores the file under a
generated name, so `attachment.FileName` is the server's storage name, not
necessarily the name you uploaded.

## Download

```csharp
byte[] bytes = await client.Attachments.DownloadAsync(attachment.Id!.Value);
```

## Replace the file

```csharp
using var stream = File.OpenRead("manual-v2.pdf");

var updated = await client.Attachments.ReplaceFileAsync(
    attachment.Id!.Value,
    new AttachmentUpload { Content = stream, FileName = "manual-v2.pdf" });
```

## Linking an attachment to a product

An attachment is standalone; to show it on a product, add it to the product's
attachments association and update the product:

```csharp
var product = await client.Products.GetAsync(productId);
product.Associations.Attachments.Add(new EntityRef(attachment.Id!.Value));
await client.Products.UpdateAsync(product);
```

:::warning

Attachment upload is affected by an open PrestaShop core bug
([#35922](https://github.com/PrestaShop/PrestaShop/issues/35922)): the file
uploads successfully, but PrestaShop's response can be malformed. The effect is
most visible when the shop runs in developer mode, where the warning is turned
into an error. Against a production-configured shop the upload works as expected.
This is a PrestaShop-side issue, not a PrestaKit one, and is outside the library's
control.

:::
