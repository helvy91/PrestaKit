using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class ManufacturerAssociations
    {
        [XmlArray("addresses")]
        [XmlArrayItem("address")]
        public List<EntityRef> Addresses { get; set; } = [];
    }
}
