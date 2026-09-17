using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class CategoryAssociations
    {
        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<EntityRef> Categories { get; set; } = [];

        [XmlArray("products")]
        [XmlArrayItem("product")]
        public List<EntityRef> Products { get; set; } = [];
    }
}
