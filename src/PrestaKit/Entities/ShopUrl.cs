using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("shop_url")]
[ApiResource("shop_urls")]
public class ShopUrl : PrestaShopEntity
{
    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("main")]
    public bool? Main { get; set; }

    [XmlElement("domain")]
    public string? Domain { get; set; }

    [XmlElement("domain_ssl")]
    public string? DomainSsl { get; set; }

    [XmlElement("physical_uri")]
    public string? PhysicalUri { get; set; }

    [XmlElement("virtual_uri")]
    public string? VirtualUri { get; set; }

    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeMain() => Main.HasValue;
    public bool ShouldSerializeDomain() => !string.IsNullOrEmpty(Domain);
    public bool ShouldSerializeDomainSsl() => !string.IsNullOrEmpty(DomainSsl);
    public bool ShouldSerializePhysicalUri() => !string.IsNullOrEmpty(PhysicalUri);
    public bool ShouldSerializeVirtualUri() => !string.IsNullOrEmpty(VirtualUri);
}