using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("carrier")]
[ApiResource("carriers")]
public class Carrier : PrestaShopEntity
{
    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("is_module")]
    public bool? IsModule { get; set; }

    [XmlElement("id_tax_rules_group")]
    public long? IdTaxRulesGroup { get; set; }

    [XmlElement("id_reference")]
    public long? IdReference { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("active")]
    public bool? Active { get; set; }

    [XmlElement("is_free")]
    public bool? IsFree { get; set; }

    [XmlElement("url")]
    public string? Url { get; set; }

    [XmlElement("shipping_handling")]
    public bool? ShippingHandling { get; set; }

    [XmlElement("shipping_external")]
    public string? ShippingExternal { get; set; }

    [XmlElement("range_behavior")]
    public bool? RangeBehavior { get; set; }

    [XmlElement("shipping_method")]
    public int? ShippingMethod { get; set; }

    [XmlElement("max_width")]
    public int? MaxWidth { get; set; }

    [XmlElement("max_height")]
    public int? MaxHeight { get; set; }

    [XmlElement("max_depth")]
    public int? MaxDepth { get; set; }

    [XmlElement("max_weight")]
    public decimal? MaxWeight { get; set; }

    [XmlElement("grade")]
    public int? Grade { get; set; }

    [XmlElement("external_module_name")]
    public string? ExternalModuleName { get; set; }

    [XmlElement("need_range")]
    public string? NeedRange { get; set; }

    [XmlElement("position")]
    public string? Position { get; set; }

    [XmlElement("delay")]
    public string? Delay { get; set; }

    public bool ShouldSerializeDeleted() => Deleted.HasValue;
    public bool ShouldSerializeIsModule() => IsModule.HasValue;
    public bool ShouldSerializeIdTaxRulesGroup() => IdTaxRulesGroup.HasValue;
    public bool ShouldSerializeIdReference() => IdReference.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeActive() => Active.HasValue;
    public bool ShouldSerializeIsFree() => IsFree.HasValue;
    public bool ShouldSerializeUrl() => !string.IsNullOrEmpty(Url);
    public bool ShouldSerializeShippingHandling() => ShippingHandling.HasValue;
    public bool ShouldSerializeShippingExternal() => !string.IsNullOrEmpty(ShippingExternal);
    public bool ShouldSerializeRangeBehavior() => RangeBehavior.HasValue;
    public bool ShouldSerializeShippingMethod() => ShippingMethod.HasValue;
    public bool ShouldSerializeMaxWidth() => MaxWidth.HasValue;
    public bool ShouldSerializeMaxHeight() => MaxHeight.HasValue;
    public bool ShouldSerializeMaxDepth() => MaxDepth.HasValue;
    public bool ShouldSerializeMaxWeight() => MaxWeight.HasValue;
    public bool ShouldSerializeGrade() => Grade.HasValue;
    public bool ShouldSerializeExternalModuleName() => !string.IsNullOrEmpty(ExternalModuleName);
    public bool ShouldSerializeNeedRange() => !string.IsNullOrEmpty(NeedRange);
    public bool ShouldSerializePosition() => !string.IsNullOrEmpty(Position);
    public bool ShouldSerializeDelay() => !string.IsNullOrEmpty(Delay);
}
