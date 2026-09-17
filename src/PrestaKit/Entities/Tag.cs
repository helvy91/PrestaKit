using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("tag")]
[ApiResource("tags")]
public class Tag : PrestaShopEntity
{
    [XmlElement("id_lang")]
    public long? IdLang { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializeName() => Name != null;
}