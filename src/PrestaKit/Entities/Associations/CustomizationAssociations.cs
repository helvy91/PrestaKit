using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class CustomizationAssociations
    {
        [XmlArray("customized_data_text_fields")]
        [XmlArrayItem("customized_data_text_field")]
        public List<CustomizedDataField> CustomizedDataTextFields { get; set; } = [];

        [XmlArray("customized_data_images")]
        [XmlArrayItem("customized_data_image")]
        public List<CustomizedDataField> CustomizedDataImages { get; set; } = [];
    }

    public sealed class CustomizedDataField
    {
        [XmlElement("id_customization_field")]
        public long IdCustomizationField { get; set; }

        [XmlElement("value")]
        public string? Value { get; set; }
    }
}
