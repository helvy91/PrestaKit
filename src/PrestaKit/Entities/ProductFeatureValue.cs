using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("product_feature_value")]
[ApiResource("product_feature_values")]
public class ProductFeatureValue : PrestaShopEntity
{
    [XmlElement("id_feature")]
    public long? IdFeature { get; set; }

    [XmlElement("custom")]
    public bool? Custom { get; set; }

    [XmlElement("value")]
    public TranslatedField? Value { get; set; }

    public bool ShouldSerializeIdFeature() => IdFeature.HasValue;
    public bool ShouldSerializeCustom() => Custom.HasValue;
    public bool ShouldSerializeValue() => Value != null;
}
