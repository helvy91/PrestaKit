using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("customer_message")]
[ApiResource("customer_messages")]
public class CustomerMessage : PrestaShopEntity
{
    [XmlElement("id_employee")]
    public long? IdEmployee { get; set; }

    [XmlElement("id_customer_thread")]
    public long? IdCustomerThread { get; set; }

    [XmlElement("ip_address")]
    public string? IpAddress { get; set; }

    [XmlElement("message")]
    public string? Message { get; set; }

    [XmlElement("file_name")]
    public string? FileName { get; set; }

    [XmlElement("user_agent")]
    public string? UserAgent { get; set; }

    [XmlElement("private")]
    public bool? Private { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    [XmlElement("read")]
    public bool? Read { get; set; }

    // Read-only fields
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue && false;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue && false;

    public bool ShouldSerializeIdEmployee() => IdEmployee.HasValue;
    public bool ShouldSerializeIdCustomerThread() => IdCustomerThread.HasValue;
    public bool ShouldSerializeIpAddress() => !string.IsNullOrEmpty(IpAddress);
    public bool ShouldSerializeMessage() => !string.IsNullOrEmpty(Message);
    public bool ShouldSerializeFileName() => !string.IsNullOrEmpty(FileName);
    public bool ShouldSerializeUserAgent() => !string.IsNullOrEmpty(UserAgent);
    public bool ShouldSerializePrivate() => Private.HasValue;
    public bool ShouldSerializeRead() => Read.HasValue;
}

    
