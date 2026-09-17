using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("customization")]
[ApiResource("customizations")]
public class Customization : PrestaShopEntity
{
    [XmlElement("id_address_delivery")]
    public long? IdAddressDelivery { get; set; }

    [XmlElement("id_cart")]
    public long? IdCart { get; set; }

    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("quantity")]
    public long? Quantity { get; set; }

    [XmlElement("quantity_refunded")]
    public long? QuantityRefunded { get; set; }

    [XmlElement("quantity_returned")]
    public long? QuantityReturned { get; set; }

    [XmlElement("in_cart")]
    public bool? InCart { get; set; }

    public bool ShouldSerializeIdAddressDelivery() => IdAddressDelivery.HasValue;
    public bool ShouldSerializeIdCart() => IdCart.HasValue;
    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeQuantity() => Quantity.HasValue;
    public bool ShouldSerializeQuantityRefunded() => QuantityRefunded.HasValue;
    public bool ShouldSerializeQuantityReturned() => QuantityReturned.HasValue;
    public bool ShouldSerializeInCart() => InCart.HasValue;
}