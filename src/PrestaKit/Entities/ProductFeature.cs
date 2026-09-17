using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("product_feature")]
[ApiResource("product_features")]
public class ProductFeature : PrestaShopEntity
{
    [XmlElement("position")]
    public int? Position { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializePosition() => Position.HasValue;
    public bool ShouldSerializeName() => Name != null;

}
