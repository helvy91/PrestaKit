using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order_invoice")]
[ApiResource("order_invoices")]
public class OrderInvoice : PrestaShopEntity
{
    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("number")]
    public long? Number { get; set; }

    [XmlElement("delivery_number")]
    public long? DeliveryNumber { get; set; }

    [XmlElement("delivery_date")]
    public DateTime? DeliveryDate { get; set; }

    [XmlElement("total_discount_tax_excl")]
    public string? TotalDiscountTaxExcl { get; set; }

    [XmlElement("total_discount_tax_incl")]
    public string? TotalDiscountTaxIncl { get; set; }

    [XmlElement("total_paid_tax_excl")]
    public string? TotalPaidTaxExcl { get; set; }

    [XmlElement("total_paid_tax_incl")]
    public string? TotalPaidTaxIncl { get; set; }

    [XmlElement("total_products")]
    public string? TotalProducts { get; set; }

    [XmlElement("total_products_wt")]
    public string? TotalProductsWt { get; set; }

    [XmlElement("total_shipping_tax_excl")]
    public string? TotalShippingTaxExcl { get; set; }

    [XmlElement("total_shipping_tax_incl")]
    public string? TotalShippingTaxIncl { get; set; }

    [XmlElement("shipping_tax_computation_method")]
    public string? ShippingTaxComputationMethod { get; set; }

    [XmlElement("total_wrapping_tax_excl")]
    public string? TotalWrappingTaxExcl { get; set; }

    [XmlElement("total_wrapping_tax_incl")]
    public string? TotalWrappingTaxIncl { get; set; }

    [XmlElement("shop_address")]
    public string? ShopAddress { get; set; }

    [XmlElement("note")]
    public string? Note { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeNumber() => Number.HasValue;
    public bool ShouldSerializeDeliveryNumber() => DeliveryNumber.HasValue;
    public bool ShouldSerializeDeliveryDate() => DeliveryDate.HasValue;
    public bool ShouldSerializeTotalDiscountTaxExcl() => !string.IsNullOrEmpty(TotalDiscountTaxExcl);
    public bool ShouldSerializeTotalDiscountTaxIncl() => !string.IsNullOrEmpty(TotalDiscountTaxIncl);
    public bool ShouldSerializeTotalPaidTaxExcl() => !string.IsNullOrEmpty(TotalPaidTaxExcl);
    public bool ShouldSerializeTotalPaidTaxIncl() => !string.IsNullOrEmpty(TotalPaidTaxIncl);
    public bool ShouldSerializeTotalProducts() => !string.IsNullOrEmpty(TotalProducts);
    public bool ShouldSerializeTotalProductsWt() => !string.IsNullOrEmpty(TotalProductsWt);
    public bool ShouldSerializeTotalShippingTaxExcl() => !string.IsNullOrEmpty(TotalShippingTaxExcl);
    public bool ShouldSerializeTotalShippingTaxIncl() => !string.IsNullOrEmpty(TotalShippingTaxIncl);
    public bool ShouldSerializeShippingTaxComputationMethod() => !string.IsNullOrEmpty(ShippingTaxComputationMethod);
    public bool ShouldSerializeTotalWrappingTaxExcl() => !string.IsNullOrEmpty(TotalWrappingTaxExcl);
    public bool ShouldSerializeTotalWrappingTaxIncl() => !string.IsNullOrEmpty(TotalWrappingTaxIncl);
    public bool ShouldSerializeShopAddress() => !string.IsNullOrEmpty(ShopAddress);
    public bool ShouldSerializeNote() => !string.IsNullOrEmpty(Note);
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
}