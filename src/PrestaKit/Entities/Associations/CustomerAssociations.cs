using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class CustomerAssociations
    {
        [XmlArray("groups")]
        [XmlArrayItem("group")]
        public List<EntityRef> Groups { get; set; } = [];
    }
}
