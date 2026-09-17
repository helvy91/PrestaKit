using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("product_option_value")]
[ApiResource("product_option_values")]
public class ProductOptionValue : PrestaShopEntity
{
    [XmlElement("id_attribute_group")]
    public long? IdAttributeGroup { get; set; }

    [XmlElement("color")]
    public string? Color { get; set; }

    [XmlElement("position")]
    public int? Position { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeIdAttributeGroup() => IdAttributeGroup.HasValue;
    public bool ShouldSerializeColor() => !string.IsNullOrEmpty(Color);
    public bool ShouldSerializePosition() => Position.HasValue;
    public bool ShouldSerializeName() => Name != null;
}