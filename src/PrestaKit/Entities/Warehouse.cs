using System.Xml.Serialization;
using PrestaKit.Entities.Associations;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("warehouse")]
[ApiResource("warehouses")]
public class Warehouse : PrestaShopEntity
{
    [XmlElement("id_address")]
    public long? IdAddress { get; set; }

    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("id_currency")]
    public long? IdCurrency { get; set; }

    [XmlElement("valuation")]
    public string? Valuation { get; set; }

    [XmlElement("deleted")]
    public string? Deleted { get; set; }

    [XmlElement("reference")]
    public string? Reference { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("management_type")]
    public string? ManagementType { get; set; }

    [XmlElement("associations")]
    public WarehouseAssociations? Associations { get; set; }

    // Read-only fields
    public static bool ShouldSerializeValuation() => false;

    public bool ShouldSerializeIdAddress() => IdAddress.HasValue;
    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeIdCurrency() => IdCurrency.HasValue;
    public bool ShouldSerializeDeleted() => !string.IsNullOrEmpty(Deleted);
    public bool ShouldSerializeReference() => !string.IsNullOrEmpty(Reference);
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeManagementType() => !string.IsNullOrEmpty(ManagementType);
    public bool ShouldSerializeAssociations() => Associations != null;
}