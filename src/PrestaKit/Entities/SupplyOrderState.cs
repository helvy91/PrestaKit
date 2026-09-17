using System.Xml.Serialization;
using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;

namespace PrestaKit.Entities;

[XmlRoot("supply_order_state")]
[ApiResource("supply_order_states")]
public class SupplyOrderState : PrestaShopEntity
{
    [XmlElement("delivery_note")]
    public bool? DeliveryNote { get; set; }

    [XmlElement("editable")]
    public bool? Editable { get; set; }

    [XmlElement("receipt_state")]
    public bool? ReceiptState { get; set; }

    [XmlElement("pending_receipt")]
    public bool? PendingReceipt { get; set; }

    [XmlElement("enclosed")]
    public bool? Enclosed { get; set; }

    [XmlElement("color")]
    public string? Color { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    public bool ShouldSerializeDeliveryNote() => DeliveryNote.HasValue;
    public bool ShouldSerializeEditable() => Editable.HasValue;
    public bool ShouldSerializeReceiptState() => ReceiptState.HasValue;
    public bool ShouldSerializePendingReceipt() => PendingReceipt.HasValue;
    public bool ShouldSerializeEnclosed() => Enclosed.HasValue;
    public bool ShouldSerializeColor() => !string.IsNullOrEmpty(Color);
    public bool ShouldSerializeName() => Name != null;
}