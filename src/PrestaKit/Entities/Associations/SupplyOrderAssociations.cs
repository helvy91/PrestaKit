using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{

    public sealed class SupplyOrderAssociations
    {
        [XmlArray("supply_order_details")]
        [XmlArrayItem("supply_order_detail")]
        public List<SupplyOrderDetail> SupplyOrderDetails { get; set; } = [];
    }

    public sealed class SupplyOrderDetail
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("id_product")]
        public long IdProduct { get; set; }

        [XmlElement("id_product_attribute")]
        public long IdProductAttribute { get; set; }

        [XmlElement("supplier_reference")]
        public string? SupplierReference { get; set; }

        [XmlElement("product_name")]
        public string? ProductName { get; set; }
    }
}
