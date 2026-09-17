using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("supply_order_detail")]
[ApiResource("supply_order_details")]
public class SupplyOrderDetail : PrestaShopEntity
{
    [XmlElement("id_supply_order")]
    public long? IdSupplyOrder { get; set; }

    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("supplier_reference")]
    public string? SupplierReference { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("ean13")]
    public string? Ean13 { get; set; }

    [XmlElement("isbn")]
    public string? Isbn { get; set; }

    [XmlElement("upc")]
    public string? Upc { get; set; }

    [XmlElement("mpn")]
    public string? Mpn { get; set; }

    [XmlElement("exchange_rate")]
    public decimal? ExchangeRate { get; set; }

    [XmlElement("unit_price_te")]
    public decimal? UnitPriceTe { get; set; }

    [XmlElement("quantity_expected")]
    public int? QuantityExpected { get; set; }

    [XmlElement("quantity_received")]
    public int? QuantityReceived { get; set; }

    [XmlElement("price_te")]
    public decimal? PriceTe { get; set; }

    [XmlElement("discount_rate")]
    public decimal? DiscountRate { get; set; }

    [XmlElement("discount_value_te")]
    public decimal? DiscountValueTe { get; set; }

    [XmlElement("price_with_discount_te")]
    public decimal? PriceWithDiscountTe { get; set; }

    [XmlElement("tax_rate")]
    public decimal? TaxRate { get; set; }

    [XmlElement("tax_value")]
    public decimal? TaxValue { get; set; }

    [XmlElement("price_ti")]
    public decimal? PriceTi { get; set; }

    [XmlElement("tax_value_with_order_discount")]
    public decimal? TaxValueWithOrderDiscount { get; set; }

    [XmlElement("price_with_order_discount_te")]
    public decimal? PriceWithOrderDiscountTe { get; set; }

    public bool ShouldSerializeIdSupplyOrder() => IdSupplyOrder.HasValue;
    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeSupplierReference() => !string.IsNullOrEmpty(SupplierReference);
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeEan13() => !string.IsNullOrEmpty(Ean13);
    public bool ShouldSerializeIsbn() => !string.IsNullOrEmpty(Isbn);
    public bool ShouldSerializeUpc() => !string.IsNullOrEmpty(Upc);
    public bool ShouldSerializeMpn() => !string.IsNullOrEmpty(Mpn);
    public bool ShouldSerializeExchangeRate() => ExchangeRate.HasValue;
    public bool ShouldSerializeUnitPriceTe() => UnitPriceTe.HasValue;
    public bool ShouldSerializeQuantityExpected() => QuantityExpected.HasValue;
    public bool ShouldSerializeQuantityReceived() => QuantityReceived.HasValue;
    public bool ShouldSerializePriceTe() => PriceTe.HasValue;
    public bool ShouldSerializeDiscountRate() => DiscountRate.HasValue;
    public bool ShouldSerializeDiscountValueTe() => DiscountValueTe.HasValue;
    public bool ShouldSerializePriceWithDiscountTe() => PriceWithDiscountTe.HasValue;
    public bool ShouldSerializeTaxRate() => TaxRate.HasValue;
    public bool ShouldSerializeTaxValue() => TaxValue.HasValue;
    public bool ShouldSerializePriceTi() => PriceTi.HasValue;
    public bool ShouldSerializeTaxValueWithOrderDiscount() => TaxValueWithOrderDiscount.HasValue;
    public bool ShouldSerializePriceWithOrderDiscountTe() => PriceWithOrderDiscountTe.HasValue;
}