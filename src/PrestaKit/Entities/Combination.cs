using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("combination")]
[ApiResource("combinations")]
public class Combination : PrestaShopEntity
{
    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("ean13")]
    public string? Ean13 { get; set; }

    [XmlElement("isbn")]
    public string? Isbn { get; set; }

    [XmlElement("upc")]
    public string? Upc { get; set; }

    [XmlElement("mpn")]
    public string? Mpn { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("supplier_reference")]
    public string? SupplierReference { get; set; }

    [XmlElement("wholesale_price")]
    public string? WholesalePrice { get; set; }

    [XmlElement("price")]
    public string? Price { get; set; }

    [XmlElement("ecotax")]
    public decimal? Ecotax { get; set; }

    [XmlElement("weight")]
    public decimal? Weight { get; set; }

    [XmlElement("unit_price_impact")]
    public string? UnitPriceImpact { get; set; }

    [XmlElement("minimal_quantity")]
    public long? MinimalQuantity { get; set; }

    [XmlElement("low_stock_threshold")]
    public int? LowStockThreshold { get; set; }

    [XmlElement("low_stock_alert")]
    public bool? LowStockAlert { get; set; }

    [XmlElement("default_on")]
    public bool? DefaultOn { get; set; }

    [XmlElement("available_now")]
    public TranslatedField? AvailableNow { get; set; }

    [XmlElement("available_later")]
    public TranslatedField? AvailableLater { get; set; }

    [XmlElement("available_date")]
    public DateTime? AvailableDate { get; set; }

    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeEan13() => !string.IsNullOrEmpty(Ean13);
    public bool ShouldSerializeIsbn() => !string.IsNullOrEmpty(Isbn);
    public bool ShouldSerializeUpc() => !string.IsNullOrEmpty(Upc);
    public bool ShouldSerializeMpn() => !string.IsNullOrEmpty(Mpn);
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeSupplierReference() => !string.IsNullOrEmpty(SupplierReference);
    public bool ShouldSerializeWholesalePrice() => !string.IsNullOrEmpty(WholesalePrice);
    public bool ShouldSerializePrice() => !string.IsNullOrEmpty(Price);
    public bool ShouldSerializeEcotax() => Ecotax.HasValue;
    public bool ShouldSerializeWeight() => Weight.HasValue;
    public bool ShouldSerializeUnitPriceImpact() => !string.IsNullOrEmpty(UnitPriceImpact);
    public bool ShouldSerializeMinimalQuantity() => MinimalQuantity.HasValue;
    public bool ShouldSerializeLowStockThreshold() => LowStockThreshold.HasValue;
    public bool ShouldSerializeLowStockAlert() => LowStockAlert.HasValue;
    public bool ShouldSerializeDefaultOn() => DefaultOn.HasValue;
    public bool ShouldSerializeAvailableNow() => AvailableNow != null;
    public bool ShouldSerializeAvailableLater() => AvailableLater != null;
    public bool ShouldSerializeAvailableDate() => AvailableDate.HasValue;
}