using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class OrderDetailAssociations
    {
        [XmlArray("taxes")]
        [XmlArrayItem("tax")]
        public List<EntityRef> Taxes { get; set; } = [];
    }

}
