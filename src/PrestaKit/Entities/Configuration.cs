using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("configuration")]
[ApiResource("configurations")]
public class Configuration : PrestaShopEntity
{
    [XmlElement("value")]
    public TranslatedField? Value { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    public bool ShouldSerializeValue() => Value != null;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
}