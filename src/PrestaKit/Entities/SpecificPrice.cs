using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("specific_price")]
[ApiResource("specific_prices")]
public class SpecificPrice : PrestaShopEntity
{
    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_cart")]
    public long? IdCart { get; set; }

    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("id_country")]
    public long? IdCountry { get; set; }

    [XmlElement("id_group")]
    public long? IdGroup { get; set; }

    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_specific_price_rule")]
    public long? IdSpecificPriceRule { get; set; }

    [XmlElement("price")]
    public string? Price { get; set; }

    [XmlElement("from_quantity")]
    public int? FromQuantity { get; set; }

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

    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdCart() => IdCart.HasValue;
    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeIdCountry() => IdCountry.HasValue;
    public bool ShouldSerializeIdGroup() => IdGroup.HasValue;
    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdSpecificPriceRule() => IdSpecificPriceRule.HasValue;
    public bool ShouldSerializePrice() => !string.IsNullOrEmpty(Price);
    public bool ShouldSerializeFromQuantity() => FromQuantity.HasValue;
    public bool ShouldSerializeReduction() => Reduction.HasValue;
    public bool ShouldSerializeReductionTax() => ReductionTax.HasValue;
    public bool ShouldSerializeReductionType() => !string.IsNullOrEmpty(ReductionType);
    public bool ShouldSerializeFrom() => From.HasValue;
    public bool ShouldSerializeTo() => To.HasValue;
}