using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Xml.Serialization;

namespace PrestaKit.Entities;

[XmlRoot("customer_thread")]
[ApiResource("customer_threads")]
public class CustomerThread : PrestaShopEntity
{
    [XmlElement("id_lang")]
    public long? IdLang { get; set; }

    [XmlElement("id_shop")]
    public long? IdShop { get; set; }

    [XmlElement("id_customer")]
    public long? IdCustomer { get; set; }

    [XmlElement("id_order")]
    public long? IdOrder { get; set; }

    [XmlElement("id_product")]
    public long? IdProduct { get; set; }

    [XmlElement("id_contact")]
    public long? IdContact { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("token")]
    public string? Token { get; set; }

    [XmlElement("status")]
    public string? Status { get; set; }

    [XmlElement("date_add")]
    public DateTime? DateAdd { get; set; }

    [XmlElement("date_upd")]
    public DateTime? DateUpd { get; set; }

    public bool ShouldSerializeIdLang() => IdLang.HasValue;
    public bool ShouldSerializeIdShop() => IdShop.HasValue;
    public bool ShouldSerializeIdCustomer() => IdCustomer.HasValue;
    public bool ShouldSerializeIdOrder() => IdOrder.HasValue;
    public bool ShouldSerializeIdProduct() => IdProduct.HasValue;
    public bool ShouldSerializeIdContact() => IdContact.HasValue;
    public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);
    public bool ShouldSerializeToken() => !string.IsNullOrEmpty(Token);
    public bool ShouldSerializeStatus() => !string.IsNullOrEmpty(Status);
    public bool ShouldSerializeDateAdd() => DateAdd.HasValue;
    public bool ShouldSerializeDateUpd() => DateUpd.HasValue;
}