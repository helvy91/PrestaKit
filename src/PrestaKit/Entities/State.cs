using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("state")]
[ApiResource("states")]
public class State : PrestaShopEntity
{
    [XmlElement("id_zone")]
    public long? IdZone { get; set; }

    [XmlElement("id_country")]
    public long? IdCountry { get; set; }

    [XmlElement("iso_code")]
    public string? IsoCode { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    public bool ShouldSerializeIdZone() => IdZone.HasValue;
    public bool ShouldSerializeIdCountry() => IdCountry.HasValue;
    public bool ShouldSerializeIsoCode() => !string.IsNullOrEmpty(IsoCode);
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeActive() => Active.HasValue;

}