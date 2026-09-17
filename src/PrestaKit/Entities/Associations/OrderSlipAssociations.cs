using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class OrderSlipAssociations
    {
        [XmlArray("order_slip_details")]
        [XmlArrayItem("order_slip_detail")]
        public List<OrderSlipDetail> OrderSlipDetails { get; set; } = [];
    }

    public sealed class OrderSlipDetail
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("id_order_detail")]
        public long IdOrderDetail { get; set; }

        [XmlElement("product_quantity")]
        public int ProductQuantity { get; set; }

        [XmlElement("amount_tax_excl")]
        public decimal AmountTaxExcl { get; set; }

        [XmlElement("amount_tax_incl")]
        public decimal AmountTaxIncl { get; set; }
    }
}
