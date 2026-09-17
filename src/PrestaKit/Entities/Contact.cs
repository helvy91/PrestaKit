using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("contact")]
[ApiResource("contacts")]
public class Contact : PrestaShopEntity
{
    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("customer_service")]
    public bool? CustomerService { get; set; }

    [XmlElement("name")]
    public TranslatedField? Name { get; set; }

    [XmlElement("description")]
    public TranslatedField? Description { get; set; }

    public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);
    public bool ShouldSerializeCustomerService() => CustomerService.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeDescription() => Description != null;
}