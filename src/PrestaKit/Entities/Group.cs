using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("group")]
[ApiResource("groups")]
public class Group : PrestaShopEntity
{
    [XmlElement("reduction")]
    public decimal? Reduction { get; set; }

    [XmlElement("price_display_method")]
    public string? PriceDisplayMethod { get; set; }

    [XmlElement("show_prices")]
    public bool? ShowPrices { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeReduction() => Reduction.HasValue;
    public bool ShouldSerializePriceDisplayMethod() => !string.IsNullOrEmpty(PriceDisplayMethod);
    public bool ShouldSerializeShowPrices() => ShowPrices.HasValue;
    public bool ShouldSerializeName() => Name != null;
}