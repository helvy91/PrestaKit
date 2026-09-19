using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("message")]
[ApiResource("messages")]
public class Message : PrestaShopEntity
{
    [XmlElement("id_cart")]
    public long? IdCart { get; set; }

    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("message")]
    public string? MessageValue { get; set; }

    [XmlElement("private")]
    public bool? Private { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;

    public bool ShouldSerializeIdCart() => IdCart.HasValue;
    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeMessageValue() => !string.IsNullOrEmpty(MessageValue);
    public bool ShouldSerializePrivate() => Private.HasValue;
}
