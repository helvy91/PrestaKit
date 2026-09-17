using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("shop_group")]
[ApiResource("shop_groups")]
public class ShopGroup : PrestaShopEntity
{
    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("color")]
    public string? Color { get; set; }

    [XmlElement("share_customer")]
    public bool? ShareCustomer { get; set; }

    [XmlElement("share_order")]
    public bool? ShareOrder { get; set; }

    [XmlElement("share_stock")]
    public bool? ShareStock { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeColor() => !string.IsNullOrEmpty(Color);
    public bool ShouldSerializeShareCustomer() => ShareCustomer.HasValue;
    public bool ShouldSerializeShareOrder() => ShareOrder.HasValue;
    public bool ShouldSerializeShareStock() => ShareStock.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
}
