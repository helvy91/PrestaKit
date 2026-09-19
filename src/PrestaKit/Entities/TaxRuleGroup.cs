using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("tax_rule_group")]
[ApiResource("tax_rule_groups")]
public class TaxRuleGroup : PrestaShopEntity
{
    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
}
