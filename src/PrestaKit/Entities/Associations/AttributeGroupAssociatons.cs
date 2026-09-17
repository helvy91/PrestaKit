using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class AttributeGroupAssociations
    {
        [XmlArray("product_option_values")]
        [XmlArrayItem("product_option_value")]
        public List<EntityRef> ProductOptionValues { get; set; } = [];
    }
}
