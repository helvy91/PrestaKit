using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class AttachmentAssociations
    {
        [XmlArray("products")]
        [XmlArrayItem("product")]
        public List<EntityRef> Products { get; set; } = [];
    }
}
