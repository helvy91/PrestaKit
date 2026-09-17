using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("order_cart_rule")]
[ApiResource("order_cart_rules")]
public class OrderCartRule : PrestaShopEntity
{
    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("id_cart_rule")]
    public long? IdCartRule { get; set; }

    [XmlElement("id_order_invoice")]
    public long? IdOrderInvoice { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("value")]
    public TranslatedField? Value { get; set; }

    [XmlElement("value_tax_excl")]
    public decimal? ValueTaxExcl { get; set; }

    [XmlElement("free_shipping")]
    public bool? FreeShipping { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeIdCartRule() => IdCartRule.HasValue;
    public bool ShouldSerializeIdOrderInvoice() => IdOrderInvoice.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeValue() => Value != null;
    public bool ShouldSerializeValueTaxExcl() => ValueTaxExcl.HasValue;
    public bool ShouldSerializeFreeShipping() => FreeShipping.HasValue;
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
}