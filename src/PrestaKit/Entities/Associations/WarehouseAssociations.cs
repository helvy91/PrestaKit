using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class WarehouseAssociations
    {
        [XmlArray("stocks")]
        [XmlArrayItem("stock")]
        public List<EntityRef> Stocks { get; set; } = [];

        [XmlArray("carriers")]
        [XmlArrayItem("carrier")]
        public List<EntityRef> Carriers { get; set; } = [];

        [XmlArray("shops")]
        [XmlArrayItem("shop")]
        public List<ShopRef> Shops { get; set; } = [];
    }
}
