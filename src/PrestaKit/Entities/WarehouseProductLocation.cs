using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("warehouse_product_location")]
[ApiResource("warehouse_product_locations")]
public class WarehouseProductLocation : PrestaShopEntity
{
    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_product_attribute")]
    public long? IdProductAttribute { get; set; }

    [XmlElement("id_warehouse")]
    public long? IdWarehouse { get; set; }

    [XmlElement("location")]
    public string? Location { get; set; }

    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdProductAttribute() => IdProductAttribute.HasValue;
    public bool ShouldSerializeIdWarehouse() => IdWarehouse.HasValue;
    public bool ShouldSerializeLocation() => !string.IsNullOrEmpty(Location);
}