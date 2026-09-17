using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("tax")]
[ApiResource("taxes")]
public class Tax : PrestaShopEntity
{
    [XmlElement("rate")]
    public decimal? Rate { get; set; }

    [XmlElement("active")]
    public string? Active { get; set; }

    [XmlElement("deleted")]
    public string? Deleted { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeRate() => Rate.HasValue;
    public bool ShouldSerializeActive() => !string.IsNullOrEmpty(Active);
    public bool ShouldSerializeDeleted() => !string.IsNullOrEmpty(Deleted);
    public bool ShouldSerializeName() => Name != null;

}