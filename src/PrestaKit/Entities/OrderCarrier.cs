using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("order_carrier")]
[ApiResource("order_carriers")]
public class OrderCarrier : PrestaShopEntity
{
    [XmlElement("id_order")]
    public long? IdOrder { get; set; } 

    [XmlElement("id_carrier")]
    public long? IdCarrier { get; set; }

    [XmlElement("id_order_invoice")]
    public long? IdOrderInvoice { get; set; } 

    [XmlElement("weight")]
    public decimal? Weight { get; set; } 

    [XmlElement("shipping_cost_tax_excl")]
    public decimal? ShippingCostTaxExcl { get; set; } 

    [XmlElement("shipping_cost_tax_incl")]
    public decimal? ShippingCostTaxIncl { get; set; } 

    [XmlElement("tracking_number")]
    public string? TrackingNumber { get; set; } 

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;

    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeIdCarrier() => IdCarrier.HasValue;
    public bool ShouldSerializeIdOrderInvoice() => IdOrderInvoice.HasValue;
    public bool ShouldSerializeWeight() => Weight.HasValue;
    public bool ShouldSerializeShippingCostTaxExcl() => ShippingCostTaxExcl.HasValue;
    public bool ShouldSerializeShippingCostTaxIncl() => ShippingCostTaxIncl.HasValue;
    public bool ShouldSerializeTrackingNumber() => !string.IsNullOrEmpty(TrackingNumber);
}