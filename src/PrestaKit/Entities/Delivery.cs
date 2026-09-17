using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("delivery")]
[ApiResource("deliveries")]
public class Delivery : PrestaShopEntity
{
    [XmlElement("id_carrier")]
    public long? IdCarrier { get; set; }

    [XmlElement("id_range_price")]
    public long? IdRangePrice { get; set; }

    [XmlElement("id_range_weight")]
    public long? IdRangeWeight { get; set; }

    [XmlElement("id_zone")]
    public long? IdZone { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("price")]
    public decimal? Price { get; set; }

    public bool ShouldSerializeIdCarrier() => IdCarrier.HasValue;
    public bool ShouldSerializeIdRangePrice() => IdRangePrice.HasValue;
    public bool ShouldSerializeIdRangeWeight() => IdRangeWeight.HasValue;
    public bool ShouldSerializeIdZone() => IdZone.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializePrice() => Price.HasValue;
}
