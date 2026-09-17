using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("cart_rule")]
[ApiResource("cart_rules")]
public class CartRule : PrestaShopEntity
{
    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("date_from")]
    public DateTime? DateFrom { get; set; }

    [XmlElement("date_to")]
    public DateTime? DateTo { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    [XmlElement("quantity")]
    public int? Quantity { get; set; }

    [XmlElement("quantity_per_user")]
    public int? QuantityPerUser { get; set; }

    [XmlElement("priority")]
    public int? Priority { get; set; }

    [XmlElement("partial_use")]
    public bool? PartialUse { get; set; }

    [XmlElement("code")]
    public string? Code { get; set; }

    [XmlElement("minimum_amount")]
    public decimal? MinimumAmount { get; set; }

    [XmlElement("minimum_amount_tax")]
    public bool? MinimumAmountTax { get; set; }

    [XmlElement("minimum_amount_currency")]
    public int? MinimumAmountCurrency { get; set; }

    [XmlElement("minimum_amount_shipping")]
    public bool? MinimumAmountShipping { get; set; }

    [XmlElement("country_restriction")]
    public bool? CountryRestriction { get; set; }

    [XmlElement("carrier_restriction")]
    public bool? CarrierRestriction { get; set; }

    [XmlElement("group_restriction")]
    public bool? GroupRestriction { get; set; }

    [XmlElement("cart_rule_restriction")]
    public bool? CartRuleRestriction { get; set; }

    [XmlElement("product_restriction")]
    public bool? ProductRestriction { get; set; }

    [XmlElement("shop_restriction")]
    public bool? ShopRestriction { get; set; }

    [XmlElement("free_shipping")]
    public bool? FreeShipping { get; set; }

    [XmlElement("reduction_percent")]
    public decimal? ReductionPercent { get; set; }

    [XmlElement("reduction_amount")]
    public decimal? ReductionAmount { get; set; }

    [XmlElement("reduction_tax")]
    public bool? ReductionTax { get; set; }

    [XmlElement("reduction_currency")]
    public long? ReductionCurrency { get; set; }

    [XmlElement("reduction_product")]
    public int? ReductionProduct { get; set; }

    [XmlElement("reduction_exclude_special")]
    public bool? ReductionExcludeSpecial { get; set; }

    [XmlElement("gift_product")]
    public long? GiftProduct { get; set; }

    [XmlElement("gift_product_attribute")]
    public long? GiftProductAttribute { get; set; }

    [XmlElement("highlight")]
    public bool? Highlight { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeDateFrom() => DateFrom.HasValue;
    public bool ShouldSerializeDateTo() => DateTo.HasValue;
    public bool ShouldSerializeDescription() => Description != null;
    public bool ShouldSerializeQuantity() => Quantity.HasValue;
    public bool ShouldSerializeQuantityPerUser() => QuantityPerUser.HasValue;
    public bool ShouldSerializePriority() => Priority.HasValue;
    public bool ShouldSerializePartialUse() => PartialUse.HasValue;
    public bool ShouldSerializeCode() => !string.IsNullOrEmpty(Code);
    public bool ShouldSerializeMinimumAmount() => MinimumAmount.HasValue;
    public bool ShouldSerializeMinimumAmountTax() => MinimumAmountTax.HasValue;
    public bool ShouldSerializeMinimumAmountCurrency() => MinimumAmountCurrency.HasValue;
    public bool ShouldSerializeMinimumAmountShipping() => MinimumAmountShipping.HasValue;
    public bool ShouldSerializeCountryRestriction() => CountryRestriction.HasValue;
    public bool ShouldSerializeCarrierRestriction() => CarrierRestriction.HasValue;
    public bool ShouldSerializeGroupRestriction() => GroupRestriction.HasValue;
    public bool ShouldSerializeCartRuleRestriction() => CartRuleRestriction.HasValue;
    public bool ShouldSerializeProductRestriction() => ProductRestriction.HasValue;
    public bool ShouldSerializeShopRestriction() => ShopRestriction.HasValue;
    public bool ShouldSerializeFreeShipping() => FreeShipping.HasValue;
    public bool ShouldSerializeReductionPercent() => ReductionPercent.HasValue;
    public bool ShouldSerializeReductionAmount() => ReductionAmount.HasValue;
    public bool ShouldSerializeReductionTax() => ReductionTax.HasValue;
    public bool ShouldSerializeReductionCurrency() => ReductionCurrency.HasValue;
    public bool ShouldSerializeReductionProduct() => ReductionProduct.HasValue;
    public bool ShouldSerializeReductionExcludeSpecial() => ReductionExcludeSpecial.HasValue;
    public bool ShouldSerializeGiftProduct() => GiftProduct.HasValue;
    public bool ShouldSerializeGiftProductAttribute() => GiftProductAttribute.HasValue;
    public bool ShouldSerializeHighlight() => Highlight.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializeName() => Name != null;
}