using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order")]
[ApiResource("orders")]
public class Order : PrestaShopEntity
{
    [XmlElement("id_address_delivery")]
    public long? IdAddressDelivery { get; set; }

    [XmlElement("id_address_invoice")]
    public long? IdAddressInvoice { get; set; } 

    [XmlElement("id_cart")]
    public long? IdCart { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; } 

    [XmlElement("id_lang")]
    public long? IdLang { get; set; } 

    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; } 

    [XmlElement("id_carrier")]
    public long? IdCarrier { get; set; } 

    [XmlElement("current_state")]
    public long? CurrentState { get; set; } 

    [XmlElement("module")]
    public string? Module { get; set; } 

    [XmlElement("invoice_number")]
    public string? InvoiceNumber { get; set; }

    [XmlElement("invoice_date")]
    public string? InvoiceDate { get; set; }

    [XmlElement("delivery_number")]
    public string? DeliveryNumber { get; set; }

    [XmlElement("delivery_date")]
    public string? DeliveryDate { get; set; }

    [XmlElement("valid")]
    public string? Valid { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; } 

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("shipping_number")]
    public string? ShippingNumber { get; set; }

    [XmlElement("note")]
    public string? Note { get; set; } 

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; } 

    [XmlElement("id_shop")]
    public long? IdShop { get; set; } 

    [XmlElement("secure_key")]
    public string? SecureKey { get; set; } 

    [XmlElement("payment")]
    public string? Payment { get; set; } 

    [XmlElement("recyclable")]
    public bool? Recyclable { get; set; } 

    [XmlElement("gift")]
    public bool? Gift { get; set; } 

    [XmlElement("gift_message")]
    public string? GiftMessage { get; set; } 

    [XmlElement("mobile_theme")]
    public bool? MobileTheme { get; set; } 

    [XmlElement("total_discounts")]
    public decimal? TotalDiscounts { get; set; } 

    [XmlElement("total_discounts_tax_incl")]
    public decimal? TotalDiscountsTaxIncl { get; set; } 

    [XmlElement("total_discounts_tax_excl")]
    public decimal? TotalDiscountsTaxExcl { get; set; } 

    [XmlElement("total_paid")]
    public decimal? TotalPaid { get; set; }

    [XmlElement("total_paid_tax_incl")]
    public decimal? TotalPaidTaxIncl { get; set; }

    [XmlElement("total_paid_tax_excl")]
    public decimal? TotalPaidTaxExcl { get; set; } 

    [XmlElement("total_paid_real")]
    public decimal? TotalPaidReal { get; set; } 

    [XmlElement("total_products")]
    public decimal? TotalProducts { get; set; }

    [XmlElement("total_products_wt")]
    public decimal? TotalProductsWt { get; set; }

    [XmlElement("total_shipping")]
    public decimal? TotalShipping { get; set; }

    [XmlElement("total_shipping_tax_incl")]
    public decimal? TotalShippingTaxIncl { get; set; }

    [XmlElement("total_shipping_tax_excl")]
    public decimal? TotalShippingTaxExcl { get; set; }

    [XmlElement("carrier_tax_rate")]
    public decimal? CarrierTaxRate { get; set; }

    [XmlElement("total_wrapping")]
    public decimal? TotalWrapping { get; set; } 

    [XmlElement("total_wrapping_tax_incl")]
    public decimal? TotalWrappingTaxIncl { get; set; } 

    [XmlElement("total_wrapping_tax_excl")]
    public decimal? TotalWrappingTaxExcl { get; set; } 

    [XmlElement("round_mode")]
    public long? RoundMode { get; set; } 

    [XmlElement("round_type")]
    public long? RoundType { get; set; } 

    [XmlElement("conversion_rate")]
    public decimal? ConversionRate { get; set; } 

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("associations")]
    public OrderAssociations? Associations { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeIdAddressDelivery() => IdAddressDelivery.HasValue;
    public bool ShouldSerializeIdAddressInvoice() => IdAddressInvoice.HasValue;
    public bool ShouldSerializeIdCart() => IdCart.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdCarrier() => IdCarrier.HasValue;
    public bool ShouldSerializeCurrentState() => CurrentState.HasValue;
    public bool ShouldSerializeModule() => !string.IsNullOrEmpty(Module);
    public bool ShouldSerializeInvoiceNumber() => !string.IsNullOrEmpty(InvoiceNumber);
    public bool ShouldSerializeInvoiceDate() => !string.IsNullOrEmpty(InvoiceDate);
    public bool ShouldSerializeDeliveryNumber() => !string.IsNullOrEmpty(DeliveryNumber);
    public bool ShouldSerializeDeliveryDate() => !string.IsNullOrEmpty(DeliveryDate);
    public bool ShouldSerializeValid() => !string.IsNullOrEmpty(Valid);
    public bool ShouldSerializeShippingNumber() => !string.IsNullOrEmpty(ShippingNumber);
    public bool ShouldSerializeNote() => !string.IsNullOrEmpty(Note);
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeSecureKey() => !string.IsNullOrEmpty(SecureKey);
    public bool ShouldSerializePayment() => !string.IsNullOrEmpty(Payment);
    public bool ShouldSerializeRecyclable() => Recyclable.HasValue;
    public bool ShouldSerializeGift() => Gift.HasValue;
    public bool ShouldSerializeGiftMessage() => !string.IsNullOrEmpty(GiftMessage);
    public bool ShouldSerializeMobileTheme() => MobileTheme.HasValue;
    public bool ShouldSerializeTotalDiscounts() => TotalDiscounts.HasValue;
    public bool ShouldSerializeTotalDiscountsTaxIncl() => TotalDiscountsTaxIncl.HasValue;
    public bool ShouldSerializeTotalDiscountsTaxExcl() => TotalDiscountsTaxExcl.HasValue;
    public bool ShouldSerializeTotalPaid() => TotalPaid.HasValue;
    public bool ShouldSerializeTotalPaidTaxIncl() => TotalPaidTaxIncl.HasValue;
    public bool ShouldSerializeTotalPaidTaxExcl() => TotalPaidTaxExcl.HasValue;
    public bool ShouldSerializeTotalPaidReal() => TotalPaidReal.HasValue;
    public bool ShouldSerializeTotalProducts() => TotalProducts.HasValue;
    public bool ShouldSerializeTotalProductsWt() => TotalProductsWt.HasValue;
    public bool ShouldSerializeTotalShipping() => TotalShipping.HasValue;
    public bool ShouldSerializeTotalShippingTaxIncl() => TotalShippingTaxIncl.HasValue;
    public bool ShouldSerializeTotalShippingTaxExcl() => TotalShippingTaxExcl.HasValue;
    public bool ShouldSerializeCarrierTaxRate() => CarrierTaxRate.HasValue;
    public bool ShouldSerializeTotalWrapping() => TotalWrapping.HasValue;
    public bool ShouldSerializeTotalWrappingTaxIncl() => TotalWrappingTaxIncl.HasValue;
    public bool ShouldSerializeTotalWrappingTaxExcl() => TotalWrappingTaxExcl.HasValue;
    public bool ShouldSerializeRoundMode() => RoundMode.HasValue;
    public bool ShouldSerializeRoundType() => RoundType.HasValue;
    public bool ShouldSerializeConversionRate() => ConversionRate.HasValue;
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeAssociations() => Associations != null;
}