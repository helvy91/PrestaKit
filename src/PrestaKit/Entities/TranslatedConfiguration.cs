using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("translated_configuration")]
[ApiResource("translated_configurations")]
public class TranslatedConfiguration : PrestaShopEntity
{
    [XmlElement("value")]
    public TranslatedField? Value { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("id_shop_group")]
    public long? IdShopGroup { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    public bool ShouldSerializeValue() => Value != null;
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeIdShopGroup() => IdShopGroup.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
}