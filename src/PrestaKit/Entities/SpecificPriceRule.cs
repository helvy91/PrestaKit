using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("specific_price_rule")]
[ApiResource("specific_price_rules")]
public class SpecificPriceRule : PrestaShopEntity
{
    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_country")]
    public long? IdCountry { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("id_group")]
    public long? IdGroup { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("from_quantity")]
    public int? FromQuantity { get; set; }

    [XmlElement("price")]
    public string? Price { get; set; }

    [XmlElement("reduction")]
    public decimal? Reduction { get; set; }

    [XmlElement("reduction_tax")]
    public bool? ReductionTax { get; set; }

    [XmlElement("reduction_type")]
    public string? ReductionType { get; set; }

    [XmlElement("from")]
    public DateTime? From { get; set; }

    [XmlElement("to")]
    public DateTime? To { get; set; }

    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdCountry() => IdCountry.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeIdGroup() => IdGroup.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeFromQuantity() => FromQuantity.HasValue;
    public bool ShouldSerializePrice() => !string.IsNullOrEmpty(Price);
    public bool ShouldSerializeReduction() => Reduction.HasValue;
    public bool ShouldSerializeReductionTax() => ReductionTax.HasValue;
    public bool ShouldSerializeReductionType() => !string.IsNullOrEmpty(ReductionType);
    public bool ShouldSerializeFrom() => From.HasValue;
    public bool ShouldSerializeTo() => To.HasValue;
}