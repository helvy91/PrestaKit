using System.Xml.Serialization;
using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("product_option")]
[ApiResource("product_options")]
public class ProductOption : PrestaShopEntity
{
    [XmlElement("is_color_group")]
    public bool? IsColorGroup { get; set; }

    [XmlElement("group_type")]
    public string? GroupType { get; set; }

    [XmlElement("position")]
    public int? Position { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("public_name")]
    public TranslatedField? PublicName { get; set; }

    [XmlElement("associations")]
    public ProductOptionAssociations? Associations { get; set; }

    public bool ShouldSerializeIsColorGroup() => IsColorGroup.HasValue;
    public bool ShouldSerializeGroupType() => !string.IsNullOrEmpty(GroupType);
    public bool ShouldSerializePosition() => Position.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializePublicName() => PublicName != null;
    public bool ShouldSerializeAssociations() => Associations != null;
}