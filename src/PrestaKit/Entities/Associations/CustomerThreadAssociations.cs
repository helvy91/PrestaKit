using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class CustomerThreadAssociations
    {
        [XmlArray("customer_messages")]
        [XmlArrayItem("customer_message")]
        public List<EntityRef> CustomerMessages { get; set; } = [];
    }
}
