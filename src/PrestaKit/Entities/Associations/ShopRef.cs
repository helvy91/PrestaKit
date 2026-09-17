using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class ShopRef
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string? Name { get; set; }
    }
}
