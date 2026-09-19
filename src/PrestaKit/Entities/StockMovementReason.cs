using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("stock_movement_reason")]
[ApiResource("stock_movement_reasons")]
public class StockMovementReason : PrestaShopEntity
{
    [XmlElement("sign")]
    public string? Sign { get; set; }

    [XmlElement("deleted")]
    public string? Deleted { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;

    public bool ShouldSerializeSign() => !string.IsNullOrEmpty(Sign);
    public bool ShouldSerializeDeleted() => !string.IsNullOrEmpty(Deleted);
    public bool ShouldSerializeName() => Name != null;
}