using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("customization_field")]
[ApiResource("product_customization_fields")]
public class CustomizationField : PrestaShopEntity
{
    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("type")]
    public long? Type { get; set; }

    [XmlElement("required")]
    public bool? Required { get; set; }

    [XmlElement("is_module")]
    public bool? IsModule { get; set; }

    [XmlElement("is_deleted")]
    public bool? IsDeleted { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeType() => Type.HasValue;
    public bool ShouldSerializeRequired() => Required.HasValue;
    public bool ShouldSerializeIsModule() => IsModule.HasValue;
    public bool ShouldSerializeIsDeleted() => IsDeleted.HasValue;
    public bool ShouldSerializeName() => Name != null;
}
