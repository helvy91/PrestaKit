using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("order_state")]
[ApiResource("order_states")]
public class OrderState : PrestaShopEntity
{
    [XmlElement("unremovable")]
    public bool? Unremovable { get; set; }

    [XmlElement("delivery")]
    public bool? Delivery { get; set; }

    [XmlElement("hidden")]
    public bool? Hidden { get; set; }

    [XmlElement("send_email")]
    public bool? SendEmail { get; set; }

    [XmlElement("module_name")]
    public string? ModuleName { get; set; }

    [XmlElement("invoice")]
    public bool? Invoice { get; set; }

    [XmlElement("color")]
    public string? Color { get; set; }

    [XmlElement("logable")]
    public bool? Logable { get; set; }

    [XmlElement("shipped")]
    public bool? Shipped { get; set; }

    [XmlElement("paid")]
    public bool? Paid { get; set; }

    [XmlElement("pdf_delivery")]
    public bool? PdfDelivery { get; set; }

    [XmlElement("pdf_invoice")]
    public bool? PdfInvoice { get; set; }

    [XmlElement("deleted")]
    public bool? Deleted { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("template")]
    public string? Template { get; set; }

    public bool ShouldSerializeUnremovable() => Unremovable.HasValue;
    public bool ShouldSerializeDelivery() => Delivery.HasValue;
    public bool ShouldSerializeHidden() => Hidden.HasValue;
    public bool ShouldSerializeSendEmail() => SendEmail.HasValue;
    public bool ShouldSerializeModuleName() => !string.IsNullOrEmpty(ModuleName);
    public bool ShouldSerializeInvoice() => Invoice.HasValue;
    public bool ShouldSerializeColor() => !string.IsNullOrEmpty(Color);
    public bool ShouldSerializeLogable() => Logable.HasValue;
    public bool ShouldSerializeShipped() => Shipped.HasValue;
    public bool ShouldSerializePaid() => Paid.HasValue;
    public bool ShouldSerializePdfDelivery() => PdfDelivery.HasValue;
    public bool ShouldSerializePdfInvoice() => PdfInvoice.HasValue;
    public bool ShouldSerializeDeleted() => Deleted.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeTemplate() => !string.IsNullOrEmpty(Template);
}