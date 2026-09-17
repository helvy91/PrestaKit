using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("tax_rule")]
[ApiResource("tax_rules")]
public class TaxRule : PrestaShopEntity
{
    [XmlElement("id_tax_rules_group")]
    public long? IdTaxRulesGroup { get; set; }

    [XmlElement("id_state")]
    public long? IdState { get; set; }

    [XmlElement("id_country")]
    public long? IdCountry { get; set; }

    [XmlElement("zipcode_from")]
    public string? ZipcodeFrom { get; set; }

    [XmlElement("zipcode_to")]
    public string? ZipcodeTo { get; set; }

    [XmlElement("id_tax")]
    public long? IdTax { get; set; }

    [XmlElement("behavior")]
    public int? Behavior { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    public bool ShouldSerializeIdTaxRulesGroup() => IdTaxRulesGroup.HasValue;
    public bool ShouldSerializeIdState() => IdState.HasValue;
    public bool ShouldSerializeIdCountry() => IdCountry.HasValue;
    public bool ShouldSerializeZipcodeFrom() => !string.IsNullOrEmpty(ZipcodeFrom);
    public bool ShouldSerializeZipcodeTo() => !string.IsNullOrEmpty(ZipcodeTo);
    public bool ShouldSerializeIdTax() => IdTax.HasValue;
    public bool ShouldSerializeBehavior() => Behavior.HasValue;
    public bool ShouldSerializeDescription() => Description != null;
}