using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("image_type")]
[ApiResource("image_types")]
public class ImageType : PrestaShopEntity
{
    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("width")]
    public string? Width { get; set; }

    [XmlElement("height")]
    public string? Height { get; set; }

    [XmlElement("categories")]
    public bool? Categories { get; set; }

    [XmlElement("products")]
    public bool? Products { get; set; }

    [XmlElement("manufacturers")]
    public bool? Manufacturers { get; set; }

    [XmlElement("suppliers")]
    public bool? Suppliers { get; set; }

    [XmlElement("stores")]
    public bool? Stores { get; set; }

    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeWidth() => !string.IsNullOrEmpty(Width);
    public bool ShouldSerializeHeight() => !string.IsNullOrEmpty(Height);
    public bool ShouldSerializeCategories() => Categories.HasValue;
    public bool ShouldSerializeProducts() => Products.HasValue;
    public bool ShouldSerializeManufacturers() => Manufacturers.HasValue;
    public bool ShouldSerializeSuppliers() => Suppliers.HasValue;
    public bool ShouldSerializeStores() => Stores.HasValue;
}