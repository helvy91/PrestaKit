using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class CartAssociations
    {
        [XmlArray("cart_rows")]
        [XmlArrayItem("cart_row")]
        public List<CartRow> CartRows { get; set; } = [];
    }

    public sealed class CartRow
    {
        [XmlElement("id_product")]
        public long IdProduct { get; set; }

        [XmlElement("id_product_attribute")]
        public long IdProductAttribute { get; set; }

        [XmlElement("id_address_delivery")]
        public long IdAddressDelivery { get; set; }

        [XmlElement("id_customization")]
        public long IdCustomization { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }
}
