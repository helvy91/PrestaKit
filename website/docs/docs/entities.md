---
sidebar_position: 7
---

# Entities

Each PrestaShop resource maps to a typed entity — `Product`, `Category`,
`Customer`, and so on. Properties are real C# types: `decimal?` for prices,
`bool?` for flags, `DateTime?` for dates, with the format quirks handled during
serialization.

## Translated fields

Multi-language fields (names, descriptions) are represented by `TranslatedField`,
not plain strings. The default language (id 1) is the implicit default:

```csharp
product.Name = "Chair";          // sets the default language (id 1)
string name = product.Name;      // reads the default language

product.Name[2] = "Krzesło";     // set a specific language by id
var polish = product.Name[2];    // read a specific language
```

Because `TranslatedField` implicitly converts to and from `string`, you can treat
it like a string for the common single-language case, and use the indexer when
you need a specific language.

## Entity references

Foreign keys to other resources are expressed with `EntityRef`, which converts
implicitly to and from `long`:

```csharp
product.IdCategoryDefault = 2;                   // a simple id property
product.Associations.Categories.Add(new EntityRef(2));   // an association
```

## Associations

Related collections (a product's categories, images, combinations, …) live under
the entity's `Associations` property:

```csharp
product.Associations.Categories.Add(new EntityRef(3));
product.Associations.Categories.Add(new EntityRef(5));

await client.Products.UpdateAsync(product);
```

## What's handled for you

When PrestaKit reads and writes entities it takes care of PrestaShop's quirks so
your values round-trip correctly:

- Empty and whitespace values become `null`.
- Zero-dates (`0000-00-00`) become `null`.
- PrestaShop's space-separated date format is converted to and from `DateTime`.
- Booleans are read and written as `0`/`1`.
- Read-only and server-managed fields (like `date_add`) are never sent on write.
- New products are created in a usable state, not left as hidden drafts.

You don't need to think about any of this — it's the point of the library.
