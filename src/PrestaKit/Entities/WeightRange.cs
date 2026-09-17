using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("weight_range")]
[ApiResource("weight_ranges")]
public class WeightRange : PrestaShopEntity
{
    [XmlElement("id_carrier")]
    public long? IdCarrier { get; set; }

    [XmlElement("delimiter1")]
    public decimal? Delimiter1 { get; set; }

    [XmlElement("delimiter2")]
    public decimal? Delimiter2 { get; set; }

    public bool ShouldSerializeIdCarrier() => IdCarrier.HasValue;
    public bool ShouldSerializeDelimiter1() => Delimiter1.HasValue;
    public bool ShouldSerializeDelimiter2() => Delimiter2.HasValue;
}
