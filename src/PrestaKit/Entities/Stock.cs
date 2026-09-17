using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("stock")]
[ApiResource("stocks")]
public class Stock : PrestaShopEntity
{
    [XmlElement("id_warehouse")]
    public long? IdWarehouse { get; set; }

    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("real_quantity")]
    public string? RealQuantity { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("ean13")]
    public string? Ean13 { get; set; }

    [XmlElement("isbn")]
    public string? Isbn { get; set; }

    [XmlElement("upc")]
    public string? Upc { get; set; }

    [XmlElement("mpn")]
    public string? Mpn { get; set; }

    [XmlElement("physical_quantity")]
    public int? PhysicalQuantity { get; set; }

    [XmlElement("usable_quantity")]
    public int? UsableQuantity { get; set; }

    [XmlElement("price_te")]
    public decimal? PriceTe { get; set; }

    // Read-only fields
    public static bool ShouldSerializeRealQuantity() => false;

    public bool ShouldSerializeIdWarehouse() => IdWarehouse.HasValue;
    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeEan13() => !string.IsNullOrEmpty(Ean13);
    public bool ShouldSerializeIsbn() => !string.IsNullOrEmpty(Isbn);
    public bool ShouldSerializeUpc() => !string.IsNullOrEmpty(Upc);
    public bool ShouldSerializeMpn() => !string.IsNullOrEmpty(Mpn);
    public bool ShouldSerializePhysicalQuantity() => PhysicalQuantity.HasValue;
    public bool ShouldSerializeUsableQuantity() => UsableQuantity.HasValue;
    public bool ShouldSerializePriceTe() => PriceTe.HasValue;
}
