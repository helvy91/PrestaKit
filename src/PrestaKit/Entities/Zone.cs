using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("zone")]
[ApiResource("zones")]
public class Zone : PrestaShopEntity
{
    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeActive() => Active.HasValue;
}
