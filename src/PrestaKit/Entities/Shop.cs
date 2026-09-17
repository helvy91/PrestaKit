using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("shop")]
[ApiResource("shops")]
public class Shop : PrestaShopEntity
{
    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("id_category")]
    public long? IdCategory { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("color")]
    public string? Color { get; set; }

    [XmlElement("theme_name")]
    public string? ThemeName { get; set; }

    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeIdCategory() => IdCategory.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeColor() => !string.IsNullOrEmpty(Color);
    public bool ShouldSerializeThemeName() => !string.IsNullOrEmpty(ThemeName);
}