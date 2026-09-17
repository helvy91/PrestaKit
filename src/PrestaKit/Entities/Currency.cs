using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("currency")]
[ApiResource("currencies")]
public class Currency : PrestaShopEntity
{
    [XmlElement("names")]
    public string? Names { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("symbol")]
    public string? Symbol { get; set; }

    [XmlElement("iso_code")]
    public string? IsoCode { get; set; }

    [XmlElement("numeric_iso_code")]
    public string? NumericIsoCode { get; set; }

    [XmlElement("precision")]
    public int? Precision { get; set; }

    [XmlElement("conversion_rate")]
    public decimal? ConversionRate { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("unofficial")]
    public bool? Unofficial { get; set; }

    [XmlElement("modified")]
    public bool? Modified { get; set; }

    [XmlElement("pattern")]
    public string? Pattern { get; set; }

    public bool ShouldSerializeNames() => !string.IsNullOrEmpty(Names);
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeSymbol() => !string.IsNullOrEmpty(Symbol);
    public bool ShouldSerializeIsoCode() => !string.IsNullOrEmpty(IsoCode);
    public bool ShouldSerializeNumericIsoCode() => !string.IsNullOrEmpty(NumericIsoCode);
    public bool ShouldSerializePrecision() => Precision.HasValue;
    public bool ShouldSerializeConversionRate() => ConversionRate.HasValue;
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeUnofficial() => Unofficial.HasValue;
    public bool ShouldSerializeModified() => Modified.HasValue;
    public bool ShouldSerializePattern() => !string.IsNullOrEmpty(Pattern);
}