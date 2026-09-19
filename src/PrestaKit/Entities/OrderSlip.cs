using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order_slip")]
[ApiResource("order_slip")]
public class OrderSlip : PrestaShopEntity
{
    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("conversion_rate")]
    public decimal? ConversionRate { get; set; }

    [XmlElement("total_products_tax_excl")]
    public decimal? TotalProductsTaxExcl { get; set; }

    [XmlElement("total_products_tax_incl")]
    public decimal? TotalProductsTaxIncl { get; set; }

    [XmlElement("total_shipping_tax_excl")]
    public decimal? TotalShippingTaxExcl { get; set; }

    [XmlElement("total_shipping_tax_incl")]
    public decimal? TotalShippingTaxIncl { get; set; }

    [XmlElement("amount")]
    public decimal? Amount { get; set; }

    [XmlElement("shipping_cost")]
    public string? ShippingCost { get; set; }

    [XmlElement("shipping_cost_amount")]
    public decimal? ShippingCostAmount { get; set; }

    [XmlElement("partial")]
    public string? Partial { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("order_slip_type")]
    public int? OrderSlipType { get; set; }

    [XmlElement("associations")] 
    public OrderSlipAssociations? Associations { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;

    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeConversionRate() => ConversionRate.HasValue;
    public bool ShouldSerializeTotalProductsTaxExcl() => TotalProductsTaxExcl.HasValue;
    public bool ShouldSerializeTotalProductsTaxIncl() => TotalProductsTaxIncl.HasValue;
    public bool ShouldSerializeTotalShippingTaxExcl() => TotalShippingTaxExcl.HasValue;
    public bool ShouldSerializeTotalShippingTaxIncl() => TotalShippingTaxIncl.HasValue;
    public bool ShouldSerializeAmount() => Amount.HasValue;
    public bool ShouldSerializeShippingCost() => !string.IsNullOrEmpty(ShippingCost);
    public bool ShouldSerializeShippingCostAmount() => ShippingCostAmount.HasValue;
    public bool ShouldSerializePartial() => !string.IsNullOrEmpty(Partial);
    public bool ShouldSerializeOrderSlipType() => OrderSlipType.HasValue;
    public bool ShouldSerializeAssociations() => Associations != null;
}
