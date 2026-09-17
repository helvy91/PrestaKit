using System.Xml.Serialization;

namespace PrestaKit.Entities.Common
{
    public abstract class PrestaShopEntity
    {
        [XmlElement("id")]
        public long? Id { get; set; }

        public bool ShouldSerializeId() => Id.HasValue;
    }
}
